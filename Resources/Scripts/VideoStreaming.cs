// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Video;
// using Freemason.Advertisements.Model;
// using Freemason.Advertisements.Controller;
// using Freemason.Timer.Controller;

// public class VideoStreaming : MonoBehaviour
// {   
//     public static VideoStreaming instance;
//     public static List<GameObject> targets = new List<GameObject>();
//     public static bool isStreaming = false;

//     private List<GameObject> targetAdvertising = new List<GameObject>();
//     private List<InformationPrefab> targetsInfo;
//     private VideoPlayer videoPlayer;
//     private VideoController videoController;
//     private TimerController timerController;

//     //Property
//     public static TimerController timer { get => (instance.timerController); set {instance.timerController = value;}}

//     private void Awake() {
//         videoPlayer = gameObject.AddComponent<VideoPlayer>();
//         videoController = gameObject.AddComponent<VideoController>();
//         timerController = gameObject.AddComponent<TimerController>();
//         instance = this;
//     }
//     private void OnEnable() {
//         targetAdvertising = targets;
//         targetsInfo = new List<InformationPrefab>();
//     }
//     private void Start()
//     {
//         Application.runInBackground = true;
//         StartCoroutine(ControlVideo());
//     }

//     private void Update() {
    
//     }

//     private IEnumerator ControlVideo() {
//         yield return videoController.FindVideos();
//         foreach(Video video in videoController.videos) {
//             isStreaming = false;
//             yield return Streaming(video);
//         }
//     }

//     private IEnumerator Streaming(Video video) {
//         videoPlayer.playOnAwake = false;
//         videoPlayer.source = VideoSource.Url;
//         videoPlayer.url = video.sources;
//         videoPlayer.SetDirectAudioMute( (ushort)0, true );
//         videoPlayer.Prepare();

//         while(!videoPlayer.isPrepared) {
//             yield return new WaitForSeconds(1);
//             break;
//         }

//         SetTextureOnTargets(videoPlayer.texture);
//         videoPlayer.Play();
//         isStreaming = videoPlayer.isPlaying;

//         while (videoPlayer.isPlaying) {
//             yield return null;
//         }
//     }

//     private void SetTextureOnTargets(Texture texture) {
//         Material[] material;
//         foreach(GameObject target in targetAdvertising) {
//             material = target.GetComponent<Renderer>().materials;
//             material[material.Length - 1].mainTexture = null;  
//             material[material.Length - 1].mainTexture = texture;  
//         }
//     }

//     private InformationPrefab OnTargetInfomation(GameObject target) => (targetsInfo.Find( x => x.instanceId == target.GetInstanceID() ));
// }