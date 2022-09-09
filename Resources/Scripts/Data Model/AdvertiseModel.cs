using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freemason.Advertisements.Model {

    public class Video { 
        private int _id;
        private string _name;
        private string _url;
        private double _length;

        public int id {
            get => (_id);
            set {_id = value;}
        }
        public string name {
            get => (_name);
            set {_name = value;}
        }
        public string url {
            get => (_url);
            set {_url = value;}
        }
        public double length {
            get => (_length);
            set {_length = value;}
        }
    }
    
    public class Image {
    }
}