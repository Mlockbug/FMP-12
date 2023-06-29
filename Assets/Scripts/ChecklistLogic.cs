using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ChecklistLogic : MonoBehaviour {
	public bool[] minigamesCompleted = new bool[5];
	public bool[] minigamesActive = new bool[5];
	public bool doneStartCutscene;
	int count;
	GameObject playButton;
	GameObject talkButton;
	bool cleared;
	Color transparent = new Color(1, 1, 1, 0);
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
		if (SceneManager.GetActiveScene().buildIndex == 13 && doneStartCutscene)
			SceneManager.LoadScene(7);
	}

	private void Update() {
		if (Keyboard.current.digit2Key.isPressed && Keyboard.current.digit6Key.isPressed) {
			ResetProgress();
		}
		playButton = GameObject.Find("Play");
		talkButton = GameObject.Find("Talk");
		activeScene = SceneManager.GetActiveScene().buildIndex;
		if (activeScene == 13 && doneStartCutscene)
			SceneManager.LoadScene(7);
		if (musicTime > 0 && musicTime != Camera.main.GetComponent<AudioSource>().time) {
			Camera.main.GetComponent<AudioSource>().time = musicTime;
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

	public void ResetProgress() {
		for (int i = 0; i < 5; i++) {
			minigamesActive[i] = false;
			minigamesCompleted[i] = false;
			GameObject.Find("Pause canvas DDOL").GetComponent<PauseLogic>().checkmarks[i].color = transparent;
		}
		doneStartCutscene = false;
		SceneManager.LoadScene(0);
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
