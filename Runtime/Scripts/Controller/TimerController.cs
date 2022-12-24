using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Freemason.Advertisements.Model;

namespace Freemason.Timer.Controller {
    public class TimerController : MonoBehaviour
    {
        //Field
        private float watchedOverview;
        private bool isTimeGoing;
        private List<GameObject> targets;
        private List<InformationPrefab> prefabsInfo;

        //Property
        public bool isGoing { get => (isTimeGoing); }
        public List<GameObject> targetsOnVisible { get => (targets); set { targets = value;} }

        private void OnEnable() {
            watchedOverview = 0f;
            isTimeGoing = false;
            targets = new List<GameObject>();
            
        }

        private void Start() {
            Application.runInBackground = true;    
        }
        private void Update() {
            this.OnWatching();
        }
        public void OnWatching () {
            isTimeGoing = (targets.Count <= 0)? false : true;
            if(!isTimeGoing) {
                
            }
        }

    }
}

