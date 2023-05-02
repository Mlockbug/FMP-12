using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseLogic : MonoBehaviour {
	int count;
	public Canvas thisCanvas;
	public GameObject pauseMenu;
	void Start() {
		count = 0;
		foreach (GameObject x in GameObject.FindGameObjectsWithTag("PauseMenu"))
			count++;
		if (count > 1 && this.gameObject.name == "Pause canvas")
			Destroy(this.gameObject);
		else {
			DontDestroyOnLoad(this);
			this.name = "Pause canvas DDOL";
		}
	}

	void Update() {
		thisCanvas.worldCamera = Camera.main;
	}

	void LoadMainMenu() {

	}

	void Resume() {

	}
}
