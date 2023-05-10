using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveLogic : MonoBehaviour {
	public GameObject mainUI;
	public GameObject moveUI;

	public void LoadRoom(int room) {
		SceneManager.LoadScene(room);
	}

	public void ShowMoveUI() {
		mainUI.SetActive(false);
		moveUI.SetActive(true);
	}

	public void ShowMainUI() {
		mainUI.SetActive(true);
		moveUI.SetActive(false);
	}

	public void PlayPoker() {
		SceneManager.LoadScene("Poker");
	}

	public void PlayCooking() {
		SceneManager.LoadScene("Cooking");
	}

	public void PlayChild() {
		SceneManager.LoadScene("Child");
	}

	public void PlayCleaning() {
		SceneManager.LoadScene("Cleaning");
	}

	public void PlayConversation() {
		SceneManager.LoadScene("Conversation");
	}
}
