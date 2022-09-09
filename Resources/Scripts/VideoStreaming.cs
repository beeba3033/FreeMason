using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Freemason.Advertisements.Model;
using Freemason.Advertisements.Controller;

public class VideoStreaming : MonoBehaviour
{   
    public static List<GameObject> targets = new List<GameObject>();

    private List<GameObject> targetAdvertising = new List<GameObject>();
    private VideoPlayer videoPlayer;
    private VideoController videoController;

    private void Awake() {
        videoPlayer = gameObject.AddComponent<VideoPlayer>();
        videoController = gameObject.AddComponent<VideoController>(); 
    }
    private void OnEnable() {
        targetAdvertising = targets;
    }
    private void Start()
    {
        Application.runInBackground = true;
        StartCoroutine(ControlVideo());
    }

    private IEnumerator ControlVideo() {
        yield return videoController.FindVideos();
        foreach(Video video in videoController.videos) {
            yield return Streaming(video);
        }
    }

    private IEnumerator Streaming(Video video) {
        videoPlayer.playOnAwake = false;
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = video.url;
        videoPlayer.SetDirectAudioMute( (ushort)0, true );
        videoPlayer.Prepare();

        while(!videoPlayer.isPrepared) {
            yield return new WaitForSeconds(1);
            break;
        }

        SetTextureOnTargets(videoPlayer.texture);
        videoPlayer.Play();

        while (videoPlayer.isPlaying) {
            yield return null;
        }
    }

    private void SetTextureOnTargets(Texture texture) {
        Material[] material;
        foreach(GameObject target in targetAdvertising) {
            material = target.GetComponent<Renderer>().materials;
            material[material.Length - 1].mainTexture = null;  
            material[material.Length - 1].mainTexture = texture;  
        }
    }
}
