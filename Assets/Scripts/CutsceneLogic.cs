using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutsceneLogic : MonoBehaviour
{
    public bool shouldPlay;
    public bool playing;
    public VideoPlayer videoPlayer;
    public VideoClip cutscene;
    float opacity;

    void Update()
    {
        if (shouldPlay && !playing) {
            videoPlayer.clip = cutscene;
            videoPlayer.Play();
            playing = true;
        }
        if (playing && !videoPlayer.isPlaying)
            opacity -= 0.05f;
        Mathf.Clamp(opacity, 0f, 1f);
        videoPlayer.targetCameraAlpha = opacity;
    }
}
