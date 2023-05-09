using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationLogic : MonoBehaviour
{
    public GameObject textBox;
    public GameObject[] buttons;
    public GameObject[] stressLevels;
    int turn;
    int stress;
    Dialogue[] textOptions;
    DialogueLogic speaker;
    string[][] allDialogue;

    [Header("Dialogue")]
    public string[] startingDiag;
	public string[] goodDiag1;  public string[] badDiag1;
    public string[] goodDiag2;  public string[] badDiag2;
    public string[] goodDiag3;	public string[] badDiag3;
    public string[] goodDiag4;  public string[] badDiag4;
    public string[] goodDiag5;  public string[] badDiag5;

    void Start()
    {
        allDialogue = new string[][]{startingDiag, badDiag1, goodDiag1, badDiag2, goodDiag2, badDiag3, goodDiag3, badDiag4, goodDiag4, badDiag5, goodDiag5};
        speaker = GameObject.Find("Dialogue Manager").GetComponent<DialogueLogic>();
        for (int i = 0; i == 10; i++) {
            textOptions[i] = gameObject.AddComponent<Dialogue>();
            textOptions[i].text = allDialogue[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (textBox.activeSelf) {
            if (turn >= 10) {
                //load old scene
            }
            else
                DisableButtons();
        }
        Mathf.Clamp(stress, 0, 4);
        stressLevels[stress].SetActive(true);
    }

    public void GoodOption(int meterLimit) {
        turn += 2;
        speaker.Speak(textOptions[turn]);
        if (meterLimit!=0)
            stress = meterLimit;
    }

	public void BadOption(int meterIncrement) {
		turn ++;
		speaker.Speak(textOptions[turn]);
        turn ++;
        stress += meterIncrement;
	}

    void DisableButtons() {
        foreach(GameObject x in buttons) {
            x.SetActive(false);
        }
        buttons[Mathf.FloorToInt(turn / 2)].SetActive(true);
    }
}
