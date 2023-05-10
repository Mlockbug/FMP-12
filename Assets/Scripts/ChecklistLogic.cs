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
		if (playButton != null)
			playButton.GetComponent<Button>().interactable = minigamesActive[SceneManager.GetActiveScene().buildIndex - 1];
		if (talkButton != null && !minigamesCompleted[SceneManager.GetActiveScene().buildIndex - 1])
			talkButton.GetComponent<Button>().onClick.AddListener(ActivateMinigame);
		if (SceneManager.GetActiveScene().buildIndex == 5 && !cleared)
			if (minigamesCompleted[SceneManager.GetActiveScene().buildIndex - 1]) {
				GameObject.Find("Background uncleaned").SetActive(false);
				cleared = true;
			}
		if (SceneManager.GetActiveScene().buildIndex != 5)
			cleared = false;
		if (SceneManager.GetActiveScene().buildIndex > 0 && SceneManager.GetActiveScene().buildIndex < 6) {
			if (minigamesCompleted[SceneManager.GetActiveScene().buildIndex - 1] && playButton != null) {
				playButton.GetComponent<Button>().interactable = false;
			}
		}
	}

	public void ActivateMinigame() {
		minigamesActive[SceneManager.GetActiveScene().buildIndex - 1] = true;
	}
	public void DeactivateMinigame(int minigame) {
		minigame--;
		minigamesActive[minigame] = false;
		minigamesCompleted[minigame] = true;
	}
}
