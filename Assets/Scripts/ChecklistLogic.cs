using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChecklistLogic : MonoBehaviour {
	public bool[] minigamesCompleted = new bool[5];
	public bool[] minigamesActive = new bool[5];
	int count;
	GameObject playButton;
	GameObject talkButton;
	bool cleared;
	int activeScene;
	void Start() {
		foreach (GameObject x in GameObject.FindGameObjectsWithTag("Checklist"))
			count++;
		if (count > 1 && this.gameObject.name == "Checklist")
			Destroy(this.gameObject);
		else {
			DontDestroyOnLoad(this);
			this.name = "Checklist DDOL";
		}
		cleared = false;
	}

	private void Update() {
		playButton = GameObject.Find("Play");
		talkButton = GameObject.Find("Talk");
		activeScene = SceneManager.GetActiveScene().buildIndex;
		if (playButton != null)
			playButton.GetComponent<Button>().interactable = minigamesActive[activeScene - 1];
		if (talkButton != null && !minigamesCompleted[activeScene - 1])
			talkButton.GetComponent<Button>().onClick.AddListener(ActivateMinigame);
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
	}
}
