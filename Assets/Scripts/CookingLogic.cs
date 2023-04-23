using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CookingLogic : MonoBehaviour {

	[Header("Stages")]
	int stage;
	public GameObject stage1;
	public GameObject stage2;
	public GameObject stage3;
	public GameObject stage4;
	public GameObject[] seasoning;
	public RectTransform[] cutLocations;
	public RectTransform knife;
	Vector3 knifeLocation;

	[Header("Animators")]
	public Animator[] animators;

	[Header("UI prompts")]
	public GameObject seasonPrompt;
	public GameObject fryPrompt;
	public GameObject cutPrompt;
	public GameObject exitPrompt;

	int seasoningCount;
	bool canSeason = true;
	int fryCount;
	bool canFry = true;
	int cutCount;
	bool canCut = true;
	bool canExit = false;
	void Start() {
		knifeLocation = knife.position;
	}
	void Update() {
		switch (stage) {
			case 0:
				if (Input.GetKeyDown(KeyCode.E) && canSeason) {
					StartCoroutine(Stage1Progress());
				}
				break;
			case 1:
				if (Input.GetKeyDown(KeyCode.F) && canFry) {
					StartCoroutine(Stage2Progress());
				}
				break;
			case 2:
				if (Input.GetKeyDown(KeyCode.C) && canCut) {
					StartCoroutine(Stage3Progress());
				}
				break;
			case 3:
				if (Input.GetKeyDown(KeyCode.Return) && canExit) {
					SceneManager.LoadScene(4);
				}
				break;
		}
		if (seasoningCount == 5) {
			seasoningCount = 0;
			stage1.SetActive(false);
			stage2.SetActive(true);
			stage = 1;
		}
		if (fryCount == 2) {
			fryCount = 0;
			stage2.SetActive(false);
			stage3.SetActive(true);
			stage = 2;
		}
		if (cutCount == 4) {
			cutCount = 0;
			stage3.SetActive(false);
			stage4.SetActive(true);
			stage = 3;
			StartCoroutine(Exit());
		}
	}
	IEnumerator Stage1Progress() {
		seasoning[seasoningCount].SetActive(true);
		seasonPrompt.SetActive(false);
		canSeason = false;
		yield return new WaitForSeconds(0.5f);
		seasoningCount++;
		canSeason = true;
		seasonPrompt.SetActive(true);
		StopCoroutine(Stage1Progress());
	}
	IEnumerator Stage2Progress() {
		canFry = false;
		animators[0].SetTrigger("flip");
		fryPrompt.SetActive(false);
		yield return new WaitForSeconds(5f);
		fryCount++;
		canFry = true;
		fryPrompt.SetActive(true);
		StopCoroutine(Stage2Progress());
	}
	IEnumerator Stage3Progress() {
		canCut = false;
		knife.position = cutLocations[cutCount].position;
		//do animation...
		cutPrompt.SetActive(false);
		yield return new WaitForSeconds(0.5f);
		knife.position = knifeLocation;
		cutCount++;
		canCut = true;
		cutPrompt.SetActive(true);
		StopCoroutine(Stage3Progress());
	}
	IEnumerator Exit() {
		yield return new WaitForSeconds(7.5f);
		exitPrompt.SetActive(true);
		canExit = true;
		StopCoroutine(Exit());
	}
}
