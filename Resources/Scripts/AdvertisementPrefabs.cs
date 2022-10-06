using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Freemason.Timer.Model;
using Freemason.Advertisements.Model;
using Freemason.Advertisements;

namespace Freemason.Advertisements.Prefabs {
    public class AdvertisementPrefabs : MonoBehaviour
    { 
        private AdvertisePlayer adPlayer;
        private InformationPrefab information;
        private TimerPrefab timer;
        private bool isVisibled;

        public AdvertisePlayer AdPlayer { get => (adPlayer); }
        public InformationPrefab Information { get => (information); }
        public TimerPrefab Timer { get => (timer); }
        
        public int groupAdvertise;

        private void Awake() {
            Application.runInBackground = true;
        }

        private void OnEnable() {
            isVisibled = false;
            information = new InformationPrefab(
                gameObject.GetInstanceID(),
                gameObject.name,
                gameObject.transform.position,
                new TimeSpan()
            );
            adPlayer = new AdvertisePlayer(this);
            timer = new TimerPrefab(this);
            Advertisements.OnTarget(this);
        }

        private void OnDisable() {
            Advertisements.NotTarget(this);
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            groupAdvertise = information.groupAdvertise;
            // Debug.Log("[" + gameObject.name + "]: " + isPlaying);
            if(isVisibled && !timer.isGoing) {
                // Debug.Log("StartTimer: " + gameObject.name);
                // timer.BeginTimer();
            }
            // test = VideoStreaming.timer.targetsOnVisible;
            // Debug.Log("isPlaying: " + VideoStreaming.isTimerGoing + " streaming: " + VideoStreaming.isStreaming);
        }
        private void OnBecameVisible() {
            // Debug.Log("Visble: " + gameObject.name);
            isVisibled = true;
            // if(OnTargetVisible() == null)
            // {
                // Debug.Log("playing: " + adPlayer.isPlaying);
                // VideoStreaming.timer.targetsOnVisible.Add(this.gameObject);
            // }
        }

        private void OnBecameInvisible() {
            isVisibled = false;
            // if(OnTargetVisible() == gameObject) {
                // VideoStreaming.timer.targetsOnVisible.Remove(this.gameObject);
            // timer.EndTimer();
            //     UpdateInformation();
            // }
        }

        private void TargetStreaming() {
            // if(OnTarget() == null)
                // VideoStreaming.targets.Add(this.gameObject);            
        }

        private void UpdateInformation () {
            // infoPrefab.instanceId = gameObject.GetInstanceID();
            // infoPrefab.name = gameObject.name;
            // infoPrefab.watched = timer.watched;
            // infoPrefab.Log();
        }

        // private GameObject OnTarget() =>  VideoStreaming.targets.Find(x => x.GetInstanceID() == gameObject.GetInstanceID());

        // private GameObject OnTargetVisible() => VideoStreaming.timer.targetsOnVisible.Find(x => x.GetInstanceID() == gameObject.GetInstanceID());
    }
}

