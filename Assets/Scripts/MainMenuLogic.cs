using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour {
	public GameObject mainMenuUI;
	public GameObject creditsUI;
	public void Play() {
		SceneManager.LoadScene(13);
	}
	public void Quit() {
		Application.Quit();
	}
	public void ShowMainMenuUI() {
		mainMenuUI.SetActive(true);
		creditsUI.SetActive(false);
	}
	public void ShowCredits() {
		mainMenuUI.SetActive(false);
		creditsUI.SetActive(true);
	}
}
