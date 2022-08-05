using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    [SerializeField]
    private string[] UrlVideos;
    
    [SerializeField]
    private GameObject[] DisplayOnObjects;

    private VideoPlayer videoPlayer;
    private VideoSource videoSource;
    private AudioSource audioSource;

    void Start()
    {
        Application.runInBackground = true;
        videoPlayer = gameObject.AddComponent<VideoPlayer>();
        audioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(ManageVideo());
    }

    IEnumerator ManageVideo()
    {
        for(int index = 0; index < UrlVideos.Length; index++) {
            yield return StartCoroutine(StreamingVideo(index));
        }
    }

    IEnumerator StreamingVideo(int index)
    {
        //Display Play on Awake for Video and Audio
        videoPlayer.playOnAwake = false;
        audioSource.playOnAwake = false;
        audioSource.Pause();

        //Want to play from url
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = UrlVideos[index];

         //Set Audio Output to AudioSource
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

        //Assign Audio from video to audiosource to be played
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);
    
        //Set video to play
        videoPlayer.Prepare();
        
        //Wait until video is prepared
        WaitForSeconds waitTime = new WaitForSeconds(1);
        while (!videoPlayer.isPrepared)
        {
            //wait for 5 sec
            yield return waitTime;
            break;
        }

        //Set materail and texture on every objects
        for(int i = 0; i < DisplayOnObjects.Length; i++) {
            DisplayOnObjects[i].GetComponent<Renderer>().material = null;
            DisplayOnObjects[i].GetComponent<Renderer>().material.mainTexture = videoPlayer.texture;
        }

        //Play Video
        videoPlayer.Play();

        //Play Sound
        audioSource.Play();

        //(Video is playing)? wait : stop
        while (videoPlayer.isPlaying)
        {
            yield return null;
        }
    }
}

/*
Note...
    videoPlayer.aspectRatio =  UnityEngine.Video.VideoAspectRatio.NoScaling; 
    videoWidth = DisplayOnObjects[i].GetComponent<Renderer>().bounds.size.x;
    videoHeight = DisplayOnObjects[i].GetComponent<Renderer>().bounds.size.y;
    DisplayOnObjects[i].GetComponent<Renderer>().material.mainTextureScale = new Vector2(transform.lossyScale.x*0.1f,transform.lossyScale.x*0.1f);
*/
