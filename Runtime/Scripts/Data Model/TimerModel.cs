using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Freemason.Advertisements.Prefabs;

namespace Freemason.Timer.Model {
    public class TimerPrefab
    {
        private float elapsed;
        private bool isTimerGoing;
        private AdvertisementPrefabs prefab;
        private static TimerPrefab instance;

        public TimeSpan timePlaying { get; set; }
        public TimeSpan watched { get; set; }
        public AdvertisementPrefabs advertisementPrefab { get => (prefab); }
        public bool isGoing { get => (isTimerGoing); }
        private bool isPosting { get; set; }

        public TimerPrefab(AdvertisementPrefabs _prefab) {
            instance = this;
            prefab = _prefab;
            watched = new TimeSpan();
            isPosting = false;
        }

        public void BeginTimer() {
            isTimerGoing = true;
            elapsed = 0f;
            prefab.StartCoroutine(UpdateTimer());
        }

        public void EndTimer() {
            isTimerGoing = false;
            watched += timePlaying;
            prefab.StopCoroutine("UpdateTimer()");
        }

        private IEnumerator UpdateTimer() {
            while(isTimerGoing)
            {
                elapsed += Time.deltaTime;
                timePlaying = TimeSpan.FromSeconds(elapsed);
                yield return null;
            }
        }

        private IEnumerator UpdatePost() {
            while(isPosting) {
                timePlaying = new TimeSpan();
                watched = new TimeSpan();
                yield return null;
            }
        }

        public static void ToPosting() {        
            instance.watched = new TimeSpan();
        }
    }
}

