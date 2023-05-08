using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueLogic : MonoBehaviour {
	public GameObject textBox;
	public GameObject mainUI;
	public Text textDisplay;
	public Text nameDisplay;
	Queue<string> fullDialogue = new Queue<string>();
	string diagString;
	bool continueText;
	public GameObject cont;
	public string[] startingText;
	public ChecklistLogic checklist;
	public GameObject currentSprite;
	public GameObject[] sprites;

	[Header("Minigame completed dialogue")]
	public string[] pokerCompleted;		Dialogue pokerText;
	public string[] cookingCompleted;	Dialogue cookingText;
	public string[] butlerCompleted;	Dialogue butlerText;
	public string[] childCompleted;		Dialogue childText;

	private void Start() {
		CreateDialogue();
		checklist = GameObject.Find("Checklist DDOL").GetComponent<ChecklistLogic>();
		if (startingText.Length != 0) {
			Dialogue startingDiag = gameObject.AddComponent<Dialogue>();
			startingDiag.text = startingText;
			Speak(startingDiag);
		}
	}

	private void Update() {
		if (fullDialogue.Count > 0) {
			if (cont.activeSelf == true && Input.GetKeyDown(KeyCode.Return)) {
				cont.SetActive(false);
				continueText = true;
			}
			if (!continueText && Input.GetKeyDown(KeyCode.Return)) {
				textDisplay.text = diagString;
				cont.SetActive(true);
			}
			if (continueText) {
				textDisplay.text = "";
				diagString = fullDialogue.Dequeue();
				if (diagString.Contains("NAME-"))
					nameDisplay.text = diagString.Split('-')[1];
				else if (diagString == "END") {
					cont.SetActive(false);
					textBox.SetActive(false);
					mainUI.SetActive(true);
				}
				else if (diagString.Contains("SPRITE-")) {
					foreach(GameObject x in sprites)
						if(x.name == diagString.Split('-')[1]) {
							currentSprite.SetActive(false);
							currentSprite = x;
							currentSprite.SetActive(true);
						}
				}
				else
					StartCoroutine(ReadText());
			}
		}
	}

	public void Speak(Dialogue text) {
		if (SceneManager.GetActiveScene().buildIndex < 7) {
			if (checklist.minigamesCompleted[SceneManager.GetActiveScene().buildIndex - 1]) {
				switch (SceneManager.GetActiveScene().buildIndex) {
					case 2:
						text = pokerText;
						break;
					case 4:
						text = cookingText;
						break;
					case 5:
						text = butlerText;
						break;
						//will have more when more dialogues get implemented
				}
			}
		}
		continueText = true;
		mainUI.SetActive(false);
		textBox.SetActive(true);
		if (fullDialogue != null)
			fullDialogue.Clear();
		foreach (string x in text.text)
			fullDialogue.Enqueue(x);
	}

	IEnumerator ReadText() {
		continueText = false;
		foreach (char x in diagString) {
			if (cont.activeSelf == false) {
				textDisplay.text += x;
				if (x != ' ')
					yield return new WaitForSeconds(0.2f);
			}
		}
		cont.SetActive(true);
	}

	void CreateDialogue() {
		//doing this here to not clutter start
		pokerText = gameObject.AddComponent<Dialogue>();	pokerText.text = pokerCompleted;
		cookingText = gameObject.AddComponent<Dialogue>();	cookingText.text = cookingCompleted;
		butlerText = gameObject.AddComponent<Dialogue>();	butlerText.text = butlerCompleted;
		childText= gameObject.AddComponent<Dialogue>();		childText.text = childCompleted;
	}
}
