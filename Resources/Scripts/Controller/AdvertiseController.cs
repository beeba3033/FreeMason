using System;
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
        private string allVideos = "https://server.beeba.ml/application/advertise_video/project/";
        private List<AdvertiseVideo> videos;

        public List<AdvertiseVideo> Videos { get => (videos); }

        public IEnumerator FindVideos() {
            int startIndex = 16;    
            int endIndex = Projects.Id.Length - startIndex;    
            string title = Projects.Id.Substring(startIndex,endIndex);
            int project = Int32.Parse(title);
            UnityWebRequest request = UnityWebRequest.Get(allVideos + project);
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                List<AdvertisingVideoModel> videoModel = JsonConvert.DeserializeObject<List<AdvertisingVideoModel>>(request.downloadHandler.text);
                videos = new List<AdvertiseVideo>();
                foreach(AdvertisingVideoModel videoObj in videoModel) {
                    videos.Add(new AdvertiseVideo(videoObj.video.id,videoObj.video.name,videoObj.video.sources));
                }
            }
        }
    }

    public class CameraController
    {
        private List<Camera> cameras;

        public List<Camera> Cameras { get => (cameras); }

        public CameraController() {
            cameras = new  List<Camera>();
            cameras.AddRange(Camera.allCameras);
        }
        
        public bool IsInView(Camera cam, GameObject toCheck) {
            Vector3 pointOnScreen = cam.WorldToScreenPoint(toCheck.GetComponentInChildren<Renderer>().bounds.center);
            // if(pointOnScreen.x < 0) {
            //     Debug.Log("from the left : " +  pointOnScreen.x);
            // }
            // else {
            //     Debug.Log("from the right : " +  pointOnScreen.x);
            // }

            // if(pointOnScreen.y < 0) {
            //     Debug.Log("from the bottom : " +  pointOnScreen.y);
            // }
            // else {
            //     Debug.Log("from the top : " +  pointOnScreen.y);
            // }
            // Debug.Log("target " + toCheck.name + " is " + pointOnScreen.x + " pixels from the left");
            //Is in front
            if(pointOnScreen.z < 0) {
                // Debug.Log("Behind: " + toCheck.name);
                return false;
            }
            
            //Is in FOV
            if( (pointOnScreen.x < 0) || (pointOnScreen.x > Screen.width) ||
                (pointOnScreen.y < 0) || (pointOnScreen.y > Screen.height) ) {
                // Debug.Log("Behind: " + toCheck.name);
                return false;        
            }
            return true;
        }
    }
}
