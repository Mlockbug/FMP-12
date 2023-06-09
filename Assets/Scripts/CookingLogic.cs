using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CookingLogic : MonoBehaviour {

	public GameObject textBox;
	Dialogue diag;
	bool inText = false;
	public string[] dialogueText;

	[Header("Stages")]
	int stage;
	public GameObject stage1;
	public GameObject stage2;
	public GameObject stage3;
	public Image saltOverlay;
	public Image pepperOverlay;
	public Image saltShaker;
	public Image pepperShaker;
	public RectTransform dial;
	public AudioSource sizzling;
	float dialRotation = 90f;
	Color transparencySalt = Color.white;
	Color transparencyPepper = Color.white;
	Color transparencyCook = Color.white;
	public Image cookedSteak;

	[Header("Animators")]
	public Animator[] animators;

	[Header("UI prompts")]
	public GameObject seasonPrompt;
	public GameObject fryPrompt;
	public GameObject exitPrompt;

	bool canSeason = true;
	float fryCount;
	float sizzVolume;
	bool cooked;
	int exitStage = 0;
	void Start() {
		transparencySalt.a = 0f;
		transparencyPepper.a = 0f;
		transparencyCook.a = 0f;
		diag = gameObject.AddComponent<Dialogue>();
		diag.text = dialogueText;
	}
	void Update() {
		switch (stage) {
			case 0:
				if (Input.GetKeyDown(KeyCode.E) && canSeason) {
					if (transparencySalt.a >= 1)
						transparencyPepper.a += 0.35f;
					else
						transparencySalt.a += 0.35f;
					if (transparencyPepper.a >= 1) {
						stage1.SetActive(false);
						stage2.SetActive(true);
						stage = 1;
					}
					StartCoroutine(Stage1Progress());
				}
				break;
			case 1:
				dialRotation += 0.05f; 
				if (Input.GetKey(KeyCode.F)) {
					dialRotation -= 0.15f;
				}
				if (fryCount >= 1) {
					cooked = true;
				}
				break;
			case 2:
				if (Input.GetKeyDown(KeyCode.Return) && exitStage == 1 && !inText && exitPrompt.activeSelf) {
					GameObject.Find("Dialogue Manager").GetComponent<DialogueLogic>().Speak(diag);
					exitPrompt.SetActive(false);
					inText = true;
				}
				if (Input.GetKeyDown(KeyCode.Return) && exitStage == 2) {
					GameObject.Find("Checklist DDOL").GetComponent<ChecklistLogic>().DeactivateMinigame(4);
					GameObject.Find("Checklist DDOL").GetComponent<ChecklistLogic>().GetTime();
					SceneManager.LoadScene(4);
				}
				break;
		}
		if (transparencySalt.a >= 1)
			pepperShaker.gameObject.SetActive(true);
		if (cooked) {
			fryCount = 0;
			stage2.SetActive(false);
			stage3.SetActive(true);
			stage = 2;
			StartCoroutine(Exit());
			cooked = false;
		}
		if (inText && !textBox.activeSelf) {
			inText= false;
			StartCoroutine(Exit());
		}
		if (dialRotation > -14.5 && dialRotation < 14.5) {
			fryCount += 0.001f;
			transparencyCook.a = fryCount;
		}
		saltOverlay.color = transparencySalt;
		pepperOverlay.color= transparencyPepper;
		cookedSteak.color = transparencyCook;
		dialRotation = Mathf.Clamp(dialRotation, -90f, 90f);
		sizzVolume = dialRotation;
		if (sizzVolume <= 0)
			sizzVolume *= -1;
		sizzling.volume = 0.9f - (sizzVolume/100f);
		dial.localRotation = Quaternion.Euler(0f, 0f, dialRotation);
	}
	IEnumerator Stage1Progress() {
		seasonPrompt.SetActive(false);
		canSeason = false;
		yield return new WaitForSeconds(0.5f);
		canSeason = true;
		seasonPrompt.SetActive(true);
		StopCoroutine(Stage1Progress());
	}
	IEnumerator Exit() {
		yield return new WaitForSeconds(2.5f);
		exitPrompt.SetActive(true);
		exitStage++;
		Debug.Log(exitStage);
		StopCoroutine(Exit());
	}
}
