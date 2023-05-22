using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChecklistLogic : MonoBehaviour {
	public bool[] minigamesCompleted = new bool[5];
	public bool[] minigamesActive = new bool[5];
	public bool doneStartCutscene;
	int count;
	GameObject playButton;
	GameObject talkButton;
	bool cleared;
	int activeScene;
	float musicTime;
	void Start() {
		foreach (GameObject x in GameObject.FindGameObjectsWithTag("Checklist"))
			count++;
		if (count > 1 && this.gameObject.name == "Checklist")
			Destroy(this.gameObject);
		else {
			DontDestroyOnLoad(this);
			Screen.SetResolution(1280, 720, true);
			this.name = "Checklist DDOL";
		}
		cleared = false;
	}

	private void Update() {
		playButton = GameObject.Find("Play");
		talkButton = GameObject.Find("Talk");
		activeScene = SceneManager.GetActiveScene().buildIndex;
		if (activeScene == 13 && doneStartCutscene)
			SceneManager.LoadScene(7);
		if (musicTime > 0 && musicTime != FindObjectOfType<AudioSource>().time) {
			FindObjectOfType<AudioSource>().time = musicTime;
			musicTime = 0;
		}
		if (playButton != null) {
			playButton.GetComponent<Button>().interactable = minigamesActive[activeScene - 1];
			if (activeScene!=1)
				playButton.GetComponent<Button>().onClick.AddListener(GetTime);
		}
		if (talkButton != null && !minigamesCompleted[activeScene - 1]) {
			if (activeScene==1)
				talkButton.GetComponent<Button>().onClick.AddListener(GetTime);
			talkButton.GetComponent<Button>().onClick.AddListener(ActivateMinigame);
		}
		if (activeScene == 5 && !cleared)
			if (minigamesCompleted[activeScene - 1]) {
				GameObject.Find("Background uncleaned").SetActive(false);
				cleared = true;
			}
		if (activeScene != 5)
			cleared = false;
		if (activeScene > 0 && activeScene < 6) {
			if (minigamesCompleted[activeScene - 1] && playButton != null) {
				playButton.GetComponent<Button>().interactable = false;
			}
		}
	}

	public void ActivateMinigame() {
		minigamesActive[activeScene - 1] = true;
	}
	public void DeactivateMinigame(int minigame) {
		minigame--;
		minigamesActive[minigame] = false;
		minigamesCompleted[minigame] = true;
		GameObject.Find("Pause canvas DDOL").GetComponent<PauseLogic>().checkmarks[minigame].color = Color.white;
	}

	public bool ShouldPlayCutscene() {
		bool status = true;
		foreach (bool x in minigamesCompleted)
			if (x == false)
				status = false;
		return status;
	}

	public void GetTime() {
		musicTime = FindObjectOfType<AudioSource>().time;
	}
}
