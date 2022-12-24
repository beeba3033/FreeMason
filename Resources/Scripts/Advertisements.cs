using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Video;
using Freemason.Advertisements.Prefabs;
using Freemason.Advertisements.Model;
using Freemason.Advertisements.Controller;
using Freemason.Prefabs.Controller;
using System.Linq;

namespace Freemason.Advertisements {
    public class Advertisements : MonoBehaviour 
    {
        private List<AdvertisementPrefabs> targets;
        private AdvertiseController advertiseController;
        private CameraController cameraController;
        private PrefabController prefabController;
        private int project;
        private bool isAlert;

        public List<AdvertisementPrefabs> Targets { get => (targets); } 
        public AdvertiseController AdvertiseController { get => (advertiseController); }
        public int LimitTargets { get => (18); }
        public bool isOverTargets { get => (targets.Count > LimitTargets); }

        private static Advertisements instance;

        private void Awake() {
            Application.runInBackground = true;
            instance = this;
            int startIndex = 16;    
            int endIndex = Projects.Id.Length - startIndex;    
            string title = Projects.Id.Substring(startIndex,endIndex);
            project = Int32.Parse(title);
            targets = new List<AdvertisementPrefabs>();
            advertiseController = new AdvertiseController(this);
            cameraController = new CameraController();
            prefabController = new PrefabController(project);
        }

        private async void OnEnable() {
            isAlert = false;
            await Task.Delay(TimeSpan.FromSeconds(0.2));
            StartCoroutine(advertiseController.SetStartAdvertising());
        }

        private void Start() {
            InvokeRepeating("PostAPI", 60, 60);
        }
        
        private void Update() {
            if(isOverTargets && !isAlert) {
                LimitPrefabs();
                isAlert = true;
            }
        }

        private void OnDisable() {
            StopCoroutine("advertiseController.SetStartAdvertising()");
        }

        public static async void  OnTarget(AdvertisementPrefabs target) {
            await Task.Delay(TimeSpan.FromSeconds(0.2));
            if(target.enabled){
                instance.targets.Add(target);
            }
        }

        public static void NotTarget(AdvertisementPrefabs target) {
            instance.targets.Remove(target);
        }

        private void LimitPrefabs() {
            EditorUtility.DisplayDialog(
                "The number of billboards exceeded the limit", 
                "You have exceeded the specified amount. Those banners will not be advertised.",
                "OK"
            );
        }

        private void PostAPI() {
            StopCoroutine("prefabController.Post()");
            prefabController.listGameObject = targets;
            StartCoroutine(prefabController.Post());
        }

        public static bool IsView(AdvertisementPrefabs target) {
            List<bool> isValue = new List<bool>();
            foreach(Camera cam in instance.cameraController.Cameras) {
                isValue.Add(instance.cameraController.IsInView(cam,target.gameObject));
            }
            return isValue.Find( value => (value == true) );
        }

    }

    public class AdvertisePlayer {
        private VideoPlayer videoPlayer;
        private AdvertisementPrefabs target;

        public VideoPlayer Video { get => (videoPlayer); }
        public bool IsPlaying { get => (videoPlayer.isPlaying); }

        public AdvertisePlayer(AdvertisementPrefabs prefab) {
            this.target = prefab;
            this.videoPlayer = target.gameObject.AddComponent<VideoPlayer>();
        }

        public IEnumerator Advertising(AdvertiseVideo video) {
            videoPlayer.playOnAwake = false;
            videoPlayer.isLooping = true;
            videoPlayer.source = VideoSource.Url;
            videoPlayer.url = video.sources;
            videoPlayer.targetTexture = RenderOnTexture();
            videoPlayer.SetDirectAudioMute( (ushort)0, true );
            videoPlayer.Prepare();
            while(!videoPlayer.isPrepared) {
                yield return new WaitForSeconds(2);
                break;
            }
            videoPlayer.Play();
            while (videoPlayer.isPlaying) {
                yield return null;
            }
        }

        private RenderTexture RenderOnTexture() {
            RenderTexture renderTexture = new RenderTexture(1280,720,0,RenderTextureFormat.ARGB32);
            // renderTexture.wrapMode = TextureWrapMode.Clamp;
            // renderTexture.filterMode = FilterMode.Bilinear;
            renderTexture.Create();
            if(videoPlayer.targetTexture != null) {
                videoPlayer.targetTexture.Release();
            }
            return renderTexture;
        }
    }

    public class AdvertiseController
    {
        private Advertisements advertisements;
        private VideoController videoController;
        private List<List<AdvertisementPrefabs>> groupGroups;
        private List<AdvertisementPrefabs> targets;
        private float limitRadius = 100f;
        private int limitNearby = 2;
        private int groupAdvertise = 1;
        private int countTarget = 0;
        private bool isAlertNearby = false;

        public VideoController VideoController { get => (videoController); } 

        public AdvertiseController (Advertisements ads) {
            this.advertisements = ads;
            this.targets = advertisements.Targets;
            this.videoController = new VideoController();
            
        }

        public IEnumerator SetStartAdvertising() {
            yield return videoController.FindVideos();
            Marketing();
            yield return null;
        }
    
        private void Marketing() { 
            int i = 0;
            int remaining = 0;
            List<AdvertiseVideo> videos = advertisements.AdvertiseController.VideoController.Videos;
            List<AdvertisementPrefabs> around;
            this.groupGroups = new List<List<AdvertisementPrefabs>>();
            foreach(AdvertisementPrefabs target in targets) {
                if(target.Information.groupAdvertise <= 0){
                    around = AroundTarget(target, limitRadius);
                    if(around.Count > 0) {
                        groupGroups.Add(around);
                        groupAdvertise++;
                    }
                }
            }
            i = GroupManage(i,videos,groupGroups);
            foreach(AdvertisementPrefabs target in targets) {
                if(countTarget >= advertisements.LimitTargets){
                    break;
                }
                if(i > videos.Count-1 ) {
                    i = 0;
                }
                if(target.Information.groupAdvertise <= remaining) {
                    StartAdvertising(target,videos[i]);
                    countTarget++;
                }
                i++;
            }
        }


        private List<AdvertisementPrefabs> AroundTarget(AdvertisementPrefabs mainTarget, float limit) {
            float distance;
            int groupAd = (int)groupAdvertise;
            AdvertisementPrefabs tempTarget  = targets.Find(x => (x.Information.instanceId == mainTarget.Information.instanceId));
            List<AdvertisementPrefabs> around = new List<AdvertisementPrefabs>();

            foreach(AdvertisementPrefabs target in targets) {
                if(mainTarget.Information.instanceId != target.Information.instanceId && target.Information.groupAdvertise <= 0) {
                    distance = Vector3.Distance(target.Information.position ,mainTarget.Information.position);
                    
                    if(distance <= limit ) {
                        around.Add(target);
                        mainTarget.Information.groupAdvertise = groupAd;
                        target.Information.groupAdvertise = groupAd;
                    }
                }
            }
            if(mainTarget.Information.groupAdvertise > 0) {
                around.Add(mainTarget);
            }
            return around;
        }

        private int GroupManage(int index, List<AdvertiseVideo> videos, List<List<AdvertisementPrefabs>> groupGroups) {
            foreach(List<AdvertisementPrefabs> group in groupGroups) {
                TargetNearby(group);
                foreach(AdvertisementPrefabs target in group) {
                    if(countTarget >= advertisements.LimitTargets) {
                        return index;
                    }
                    StartAdvertising(target,videos[index]);
                    countTarget++;
                }                    
                index++;
                if(index >= videos.Count) { index = 0; }
            }
            //return index next to play video
            return index;
        }

        private void StartAdvertising(AdvertisementPrefabs target,AdvertiseVideo video) {
            target.StartCoroutine(target.AdPlayer.Advertising(video));
        }

        private void TargetNearby(List<AdvertisementPrefabs> group) {
            if(group.Count >= limitNearby && !isAlertNearby) {
                EditorUtility.DisplayDialog(
                        "The Banner is too close", 
                        "The ads will be projected the same way or not at all if the signs are close to each other within a specified distance.",
                        "OK"
                );
                isAlertNearby = true;
            }
        }
    }
}