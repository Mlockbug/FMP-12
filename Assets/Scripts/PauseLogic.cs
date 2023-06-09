using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseLogic : MonoBehaviour {
	int count;
	public Canvas thisCanvas;
	public GameObject pauseMenu;
	public Image[] checkmarks;
	bool playingCutscene;
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
		if (SceneManager.GetActiveScene().buildIndex == 6)
			playingCutscene = Camera.main.gameObject.GetComponent<CutsceneLogic>().playing;
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (SceneManager.GetActiveScene().buildIndex != 0 && !playingCutscene && SceneManager.GetActiveScene().buildIndex != 13)
				pauseMenu.SetActive(true);
		}
	}

	public void LoadMainMenu() {
		pauseMenu.SetActive(false);
		SceneManager.LoadScene(0);
	}

	public void Resume() {
		pauseMenu.SetActive(false);
	}
}
