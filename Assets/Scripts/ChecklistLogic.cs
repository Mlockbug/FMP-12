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
	void Start() {
		foreach (GameObject x in GameObject.FindGameObjectsWithTag("Checklist"))
			count++;
		if (count > 1 && this.gameObject.name == "Checklist")
			Destroy(this.gameObject);
		else {
			DontDestroyOnLoad(this);
			this.name = "Checklist DDOL";
		}
	}

	private void Update() {
		playButton = GameObject.Find("Play");
		talkButton = GameObject.Find("Talk");
		if (playButton != null)
			playButton.GetComponent<Button>().interactable = minigamesActive[SceneManager.GetActiveScene().buildIndex - 1];
		if (talkButton != null && !minigamesCompleted[SceneManager.GetActiveScene().buildIndex - 1])
			talkButton.GetComponent<Button>().onClick.AddListener(ActivateMinigame);
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
