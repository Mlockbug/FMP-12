using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningLogic : MonoBehaviour {
	public string[] progressText;	Dialogue progressDiag;
	public string[] frameText;		Dialogue frameDiag;
	public string[] bearText;		Dialogue bearDiag;
	public string[] houseText;		Dialogue houseDiag;
	public string[] pillText;		Dialogue pillDiag;
	public string[] completeText;	Dialogue completeDiag;
	void Start() {
		progressDiag = gameObject.AddComponent<Dialogue>();	progressDiag.text = progressText;
		frameDiag = gameObject.AddComponent<Dialogue>();	frameDiag.text = frameText;
		bearDiag = gameObject.AddComponent<Dialogue>();		bearDiag.text = bearText;
		houseDiag = gameObject.AddComponent<Dialogue>();	houseDiag.text = houseText;
		pillDiag = gameObject.AddComponent<Dialogue>();		pillDiag.text = pillText;
		completeDiag = gameObject.AddComponent<Dialogue>();	completeDiag.text = completeText;
	}

	void Update() {

	}
}
