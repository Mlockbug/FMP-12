using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour {
	public void Play() {
		SceneManager.LoadScene(6);
	}
	public void Quit() {
		Application.Quit();
	}
}
