using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using Freemason.Advertisements.Model;

namespace Freemason.Advertisements.Controller {
    public class VideoController : MonoBehaviour {
        private List<Video> _videos;
        private void Start() {
            Application.runInBackground = true;    
        }

        public IEnumerator FindVideos() {
            UnityWebRequest request = UnityWebRequest.Get("http://localhost:8080/video/all");
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                _videos = JsonConvert.DeserializeObject<List<Video>>(request.downloadHandler.text);
            }
        }

        public List<Video> videos {
            get => (_videos);
        }
    }

}
