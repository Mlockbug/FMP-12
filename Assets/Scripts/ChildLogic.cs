using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChildLogic : MonoBehaviour {
	public GameObject[] failButtons;
	public GameObject[] succeedButtons;
	int roundsWon;
	bool newRound = true;
	int position;
	int previousPosition = 0;
	public GameObject textBox;
	public string[] failText;
	Dialogue failDiag;
	public string[] succeedText;
	Dialogue succeedDiag;
	bool successDiagCompleted = false;
	bool inDiag;
	// Start is called before the first frame update
	void Start() {
		failDiag = gameObject.AddComponent<Dialogue>(); failDiag.text = failText;
		succeedDiag = gameObject.AddComponent<Dialogue>(); succeedDiag.text = succeedText;
	}

	// Update is called once per frame
	void Update() {
		if (roundsWon == 5) {
			newRound = false;
			if (successDiagCompleted) {
				GameObject.Find("Checklist DDOL").GetComponent<ChecklistLogic>().DeactivateMinigame(3);
				GameObject.Find("Checklist DDOL").GetComponent<ChecklistLogic>().GetTime();
				SceneManager.LoadScene(3);
			}
			if (!inDiag) {
				GameObject.Find("Dialogue Manager").GetComponent<DialogueLogic>().Speak(succeedDiag);
				inDiag = true;
			}
		}
		if (newRound && !textBox.activeSelf) {
			if (previousPosition == position)
				position = Random.Range(0, 4);
			else {
				newRound = false;
				foreach (GameObject x in failButtons)
					x.SetActive(true);
				foreach (GameObject y in succeedButtons)
					y.SetActive(false);
				failButtons[position].SetActive(false);
				succeedButtons[position].SetActive(true);
				previousPosition = position;
			}
		}
		if (inDiag && !textBox.activeSelf) {
			inDiag = false;
			successDiagCompleted = true;
		}
	}

	public void Fail() {
		roundsWon = 0;
		newRound = true;
		GameObject.Find("Dialogue Manager").GetComponent<DialogueLogic>().Speak(failDiag);
	}

	public void Succeed() {
		roundsWon++;
		newRound = true;
	}
}
