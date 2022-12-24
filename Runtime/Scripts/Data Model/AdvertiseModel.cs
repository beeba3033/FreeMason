using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freemason.Advertisements.Model {

    public class AdvertisingVideoModel {
        public GameAdsModel project { get; set; }
        public VideoAdsModel video { get; set; }
    }

    public class GameAdsModel {
        public int id { get; set; }
        public string description { get; set; }
        public string name { get; set; } 
    }

    public class VideoAdsModel {
        public int id { get;set;}
        public string name { get; set; }
        public string nameAds { get; set; }
        public string sources { get; set; }
    }

    public class AdvertiseVideo { 
        private int _id;
        private string _name;
        private string _source;
        private double _length;

        public AdvertiseVideo (
            int videoId,
            string videoName,
            string videoSource
        ) {
            _id = videoId;
            _name = videoName;
            _source = videoSource;
        }

        public int id {
            get => (_id);
            set {_id = value;}
        }
        public string name {
            get => (_name);
            set {_name = value;}
        }
        public string sources {
            get => (_source);
            set {_source = value;}
        }
        public double length {
            get => (_length);
            set {_length = value;}
        }
    }
    
    public class Image {
    }

    public class InformationPrefab
    {
        public int instanceId { get; set; }
        public int groupAdvertise { get; set; }
        public string name { get; set; }
        public Vector3 position;
        public TimeSpan watched { get;set; }
        public TimeSpan lastest { get;set; }

        public InformationPrefab (
            int _intanceId,
            string _name,
            Vector3 _position,
            TimeSpan _watched,
            TimeSpan _lastest
        ) {
            instanceId = _intanceId;
            name = _name;
            position = _position;
            watched = _watched;
            lastest = _lastest;
            groupAdvertise = 0;
        }

        public void Log () {
            Debug.Log(
                "intanceId: " + instanceId +"\n" + 
                "name: " + name +"\n" +
                "watched: " + watched +"\n" + 
                "groupAdvertise: "+ groupAdvertise + "\n"+
                "{\n x: " + position.x +"\n" + 
                " y: " + position.y +"\n" +
                " z: " + position.z +"\n}" 
            );
        }
    }

}