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
        private TimeSpan timePlaying;
        private TimeSpan _watched;
        private AdvertisementPrefabs prefab;

        public TimeSpan watched { get => (_watched); }
        public AdvertisementPrefabs advertisementPrefab { get => (prefab); }
        public bool isGoing { get => (isTimerGoing); }

        public TimerPrefab(AdvertisementPrefabs _prefab) {
            prefab = _prefab;
            _watched = new TimeSpan();
        }

        public void BeginTimer() {
            isTimerGoing = true;
            elapsed = 0f;
            prefab.StartCoroutine(UpdateTimer());
        }

        public void EndTimer() {
            isTimerGoing = false;
            _watched += timePlaying;
            prefab.StopCoroutine("UpdateTimer()");

            Debug.Log(
                "name: " + prefab.name + "\n" +
                "watchedTime: " + watched + "\n"
            );
        }

        private IEnumerator UpdateTimer() {
            while(isTimerGoing)
            {
                elapsed += Time.deltaTime;
                timePlaying = TimeSpan.FromSeconds(elapsed);
                yield return null;
            }
        }

        // private bool watchingStreaming() => (VideoStreaming.isStreaming && VideoStreaming.timer.isGoing); 

    }
}

