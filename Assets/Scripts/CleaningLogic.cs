using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CleaningLogic : MonoBehaviour {
	public string[] progressText;	Dialogue progressDiag;
	public string[] frameText;		Dialogue frameDiag;
	public string[] bearText;		Dialogue bearDiag;
	public string[] houseText;		Dialogue houseDiag;
	public string[] pillText;		Dialogue pillDiag;
	public string[] completeText;	Dialogue completeDiag;
	GameObject heldObject;
	public bool pickingUp = false;
	Vector3 offset;
	int objectsCount;
	DialogueLogic dialogueManager;
	public GameObject textBox;
	bool doneProgressDiag = false;
	bool doneCompleteDiag = false;
	void Start() {
		progressDiag = gameObject.AddComponent<Dialogue>();	progressDiag.text = progressText;
		frameDiag = gameObject.AddComponent<Dialogue>();	frameDiag.text = frameText;
		bearDiag = gameObject.AddComponent<Dialogue>();		bearDiag.text = bearText;
		houseDiag = gameObject.AddComponent<Dialogue>();	houseDiag.text = houseText;
		pillDiag = gameObject.AddComponent<Dialogue>();		pillDiag.text = pillText;
		completeDiag = gameObject.AddComponent<Dialogue>();	completeDiag.text = completeText;
		dialogueManager = GameObject.FindObjectOfType<DialogueLogic>();
	}

	void Update() {
		if (Input.GetMouseButtonDown(0) && heldObject != null) {
			pickingUp = true;
			offset = FindObjectOfType<Camera>().ScreenToWorldPoint(Input.mousePosition) - heldObject.GetComponent<RectTransform>().position;
		}
		if (Input.GetMouseButton(0) && heldObject != null && pickingUp) {
			pickingUp = true;
			heldObject.GetComponent<RectTransform>().position = new Vector3(FindObjectOfType<Camera>().ScreenToWorldPoint(Input.mousePosition).x, FindObjectOfType<Camera>().ScreenToWorldPoint(Input.mousePosition).y, 0f) - offset;
		}
		if (Input.GetMouseButtonUp(0)) {
			pickingUp = false;
			heldObject = null;
			offset = Vector3.zero;
		}

		if (!textBox.activeSelf && objectsCount == 18 && !doneProgressDiag) {
			dialogueManager.Speak(progressDiag);
			doneProgressDiag = true;
		}
		else if (!textBox.activeSelf && doneCompleteDiag) {
			GameObject.Find("Checklist DDOL").GetComponent<ChecklistLogic>().DeactivateMinigame(5);
			SceneManager.LoadScene(5);
		}
		else if (!textBox.activeSelf && objectsCount == 27) {
			dialogueManager.Speak(completeDiag);
			doneCompleteDiag = true;
		}
	}

	public void Pickup(GameObject parent) {
		if (!pickingUp) {
			heldObject = parent.transform.parent.gameObject;
		}
	}

	void OnTriggerEnter2D(Collider2D collision) {
		Debug.Log("EEEEEEEEEEEEE");
		switch (collision.gameObject.tag) {
			case "Frame":
				dialogueManager.Speak(frameDiag);
				break;
			case "Bear":
				dialogueManager.Speak(bearDiag);
				break;
			case "House":
				dialogueManager.Speak(houseDiag);
				break;
			case "Pill":
				dialogueManager.Speak(pillDiag);
				break;
		}
		objectsCount++;
		Destroy(collision.transform.parent.gameObject);
	}
}
