using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advertisements.Object {
    public class AdvertiseObject : MonoBehaviour
    {
        private void OnEnable() {
            if(VideoStreaming.targets.Find(x => x.GetInstanceID() == gameObject.GetInstanceID()) != null);
                VideoStreaming.targets.Add(gameObject);
        }
        private void Awake() {
        }
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        private void OnBecameVisible() {
            Debug.Log("See " + gameObject.name);
        }

         private void OnBecameInvisible() {
            Debug.Log("Not to see " + gameObject.name);
         }
    }
}

