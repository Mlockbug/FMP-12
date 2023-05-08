using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationLogic : MonoBehaviour
{
    public GameObject textBox;
    public GameObject[] buttons;
    int turn;
    int stress;
    public Dialogue[] textOptions;
    DialogueLogic speaker;
    string[][] allDialogue;
    public string[] startingDiag;
	public string[] goodDiag1;
    public string[] goodDiag2;
    public string[] goodDiag3;
    public string[] badDiag1;
	public string[] badDiag2;
	public string[] badDiag3;
	void Start()
    {
        allDialogue = new string[][]{startingDiag, badDiag1, goodDiag1, badDiag2, goodDiag2, badDiag3, goodDiag3};
        speaker = GameObject.Find("Dialogue Manager").GetComponent<DialogueLogic>();
        for (int i = 0; i == 6; i++) {
            textOptions[i] = gameObject.AddComponent<Dialogue>();
            textOptions[i].text = allDialogue[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (textBox.activeSelf) {
            if (turn >= 6) {
                //load old scene
            }
            else
                DisableButtons();
        }
    }

    public void GoodOption() {
        turn += 2;
        speaker.Speak(textOptions[turn]);
    }

	public void BadOption() {
		turn ++;
		speaker.Speak(textOptions[turn]);
        turn ++;
	}

    void DisableButtons() {
        foreach(GameObject x in buttons) {
            x.SetActive(false);
        }
        buttons[Mathf.FloorToInt(turn / 2)].SetActive(true);
    }
}
