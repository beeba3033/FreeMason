using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Freemason.Advertisements;
using Freemason.Advertisements.Model;
using Freemason.Timer.Model;

namespace Freemason.Advertisements.Prefabs {
    public class AdvertisementPrefabs : MonoBehaviour
    { 
        private AdvertisePlayer adPlayer;
        private InformationPrefab information;
        private TimerPrefab timer;
        private bool isVisible;

        public AdvertisePlayer AdPlayer { get => (adPlayer); }
        public InformationPrefab Information { get => (information); }
        public TimerPrefab Timer { get => (timer); }

        private void Awake() {
            Application.runInBackground = true;
        }

        private void OnEnable() {
            isVisible = false;
            TimeSpan currentTime = new TimeSpan();
            information = new InformationPrefab(
                gameObject.GetInstanceID(),
                gameObject.name,
                gameObject.transform.position,
                currentTime,
                currentTime
            );
            adPlayer = new AdvertisePlayer(this);
            timer = new TimerPrefab(this);
            Advertisements.OnTarget(this);
        }

        private void OnDisable() {
            Advertisements.NotTarget(this);
        }

        private void Start()
        {

        }

        private void Update()
        {
            if(isVisible && adPlayer.IsPlaying) {
                timer.BeginTimer();
            }
            information.position = gameObject.transform.position;
            information.watched = timer.watched;
        }

        private void OnBecameVisible() {
            isVisible = true;
            Advertisements.IsView(this);
        }

        private void OnBecameInvisible() {
            isVisible = false;
            timer.EndTimer();
        }
    }
}

