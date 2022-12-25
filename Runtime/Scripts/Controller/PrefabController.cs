using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Freemason.Advertisements.Model;
using  System.Text;
using System.Text.Json;
using Newtonsoft.Json.Converters;
using Freemason.Advertisements.Prefabs;

namespace Freemason.Prefabs.Controller {

    [Serializable]
    public class GameObjectForm {
        public int id;
        public string name;
        public double positionX;
        public double positionY;
        public double positionZ;
        public DateTime watching;

        public GameObjectForm(
            int Id,
            string Name,
            double PositionX,
            double PositionY,
            double PositionZ,
            TimeSpan Watched
        ) {
            id = Id;
            name = Name;
            positionX = PositionX;
            positionY = PositionY;
            positionZ = PositionZ;
            watching = DateTime.Today + Watched;
            // watching.Add(Watched);
        }
    }

    public class PrefabController
    {
        private int projectRepository;
        public List<AdvertisementPrefabs> listGameObject { get; set; }

        public PrefabController(int id) {
            projectRepository = id;
        }
        
        public IEnumerator Post() {
            Freemason.Timer.Model.TimerPrefab.ToPosting();
            List<GameObjectForm> forms = new List<GameObjectForm>();
            foreach(AdvertisementPrefabs obj in listGameObject) {
                forms.Add(new GameObjectForm(
                    obj.GetInstanceID(),
                    obj.gameObject.name,
                    obj.Information.position.x,
                    obj.Information.position.y,
                    obj.Information.position.z,
                    obj.Information.watched
                ));
            }
            GameObjectForm[] arrayForms = forms.ToArray();
            string json = JsonConvert.SerializeObject(arrayForms);
            JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings();
            string microsoftJson = JsonConvert.SerializeObject(arrayForms, microsoftDateFormatSettings);            
            string url = ("https://server.beeba.ml/plugin/project/"+ projectRepository +"/objects/save");
            UnityWebRequest request = new UnityWebRequest();
            request.url = url;
            request.method = UnityWebRequest.kHttpVerbPOST;  
            request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
            request.SetRequestHeader("Accept", "application/json");
            request.SetRequestHeader("Accept-Encoding", "gzip, deflate, br");
            request.downloadHandler = new DownloadHandlerBuffer();
            request.uploadHandler = new UploadHandlerRaw(string.IsNullOrEmpty(microsoftJson) ? null : Encoding.UTF8.GetBytes(microsoftJson));
            Debug.Log(request.uploadHandler);
            yield return request.SendWebRequest();
            if(request.result != UnityWebRequest.Result.Success) {
                Debug.Log(request.downloadHandler.text);
                Debug.Log(request.error);
            }
            else {
                string results = request.downloadHandler.text;
                Debug.Log(results);
            }
            request.Dispose();
            yield return null;
        }

        TimeSpan DateTimeToTimeSpan(DateTime? ts)
        {
            if (!ts.HasValue) return TimeSpan.Zero;
            else return new TimeSpan(0, ts.Value.Hour, ts.Value.Minute, ts.Value.Second, ts.Value.Millisecond);
        }
    }
}