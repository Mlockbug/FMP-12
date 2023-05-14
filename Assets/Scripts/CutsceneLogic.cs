using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutsceneLogic : MonoBehaviour
{
    public bool shouldPlay;
    public bool playing = false;
    public VideoPlayer videoPlayer;
    public VideoClip cutscene;
    float opacity;
    public GameObject canvas;
    public GameObject endScreen;

	void Start() {
		shouldPlay = GameObject.Find("Checklist DDOL").GetComponent<ChecklistLogic>().ShouldPlayCutscene();
	}
	void Update()
    {
        if (playing != videoPlayer.isPlaying && Application.isFocused) {
            Debug.Log(videoPlayer.isPlaying);
            Debug.Log(playing);
            StartCoroutine(ShowEndScreen());
        }
		if (shouldPlay && !playing) {
            canvas.SetActive(false);
            endScreen.SetActive(false);
            videoPlayer.clip = cutscene;
            videoPlayer.Play();
            playing = true;
        }
		else videoPlayer.targetCameraAlpha = 1;
		playing = videoPlayer.isPlaying;
        
    }

    IEnumerator ShowEndScreen() {
        shouldPlay= false;
        playing= false;
        while (opacity>0f) {
			if (!videoPlayer.isPlaying)
				opacity -= 0.05f;
			Mathf.Clamp(opacity, 0f, 1f);
			videoPlayer.targetCameraAlpha = opacity;
		}
        endScreen.SetActive(true);
        yield return new WaitForSeconds(5f);
        Application.Quit();
    }
}
