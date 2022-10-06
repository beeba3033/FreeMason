using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using Freemason.Advertisements.Model;

namespace Freemason.Advertisements.Controller {
    public class VideoController
    {
        private string allVideos = "http://localhost:8080/video/all";
        private List<AdvertiseVideo> videos;

        public List<AdvertiseVideo> Videos { get => (videos); }

        public IEnumerator FindVideos() {
            UnityWebRequest request = UnityWebRequest.Get(allVideos);
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                videos = JsonConvert.DeserializeObject<List<AdvertiseVideo>>(request.downloadHandler.text);
            }
        }
    
    }
}
