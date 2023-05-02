using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChildLogic : MonoBehaviour {
	public GameObject[] failButtons;
	public GameObject[] succeedButtons;
	int roundsWon;
	bool newRound = true;
	int position;
	public GameObject textBox;
	public string[] failText;
	Dialogue failDiag;
	public string[] succeedText;
	Dialogue succeedDiag;
	// Start is called before the first frame update
	void Start() {
		failDiag = this.gameObject.AddComponent<Dialogue>();  failDiag.text = failText;
		succeedDiag = this.gameObject.AddComponent<Dialogue>();  succeedDiag.text = succeedText;
	}

	// Update is called once per frame
	void Update() {
		if (roundsWon== 5) {
			//dialogue
			//go to room
			Debug.Log("done");
			newRound= false;
		}
		if (newRound && !textBox.activeSelf) {
			newRound = false;
			position = Random.Range(0, 4);
			foreach (GameObject x in failButtons)
				x.SetActive(true);
			foreach (GameObject y in succeedButtons)
				y.SetActive(false);
			failButtons[position].SetActive(false);
			succeedButtons[position].SetActive(true);
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
