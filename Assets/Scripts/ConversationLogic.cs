using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConversationLogic : MonoBehaviour
{
    public GameObject textBox;
    public GameObject[] buttons;
    public GameObject[] stressLevels;
    int turn;
    int stress;
    Dialogue[] textOptions = new Dialogue[11];
    DialogueLogic speaker = null;
    string[][] allDialogue;
    public GameObject idleSprite;
    public GameObject angerSprite1;
    public GameObject angerSprite2;
    bool inText;

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
        for (int i = 0; i < 11; i++) {
            textOptions[i] = gameObject.AddComponent<Dialogue>();
            textOptions[i].text = allDialogue[i];
        }
    }

    void Update()
    {
        if (speaker == null)
            speaker = GameObject.Find("Dialogue Manager").GetComponent<DialogueLogic>();
        if (!textBox.activeSelf && turn >= 10 && !inText) {
            GameObject.Find("Checklist DDOL").GetComponent<ChecklistLogic>().DeactivateMinigame(1);
			GameObject.Find("Checklist DDOL").GetComponent<ChecklistLogic>().GetTime();
			SceneManager.LoadScene(1);
        }
        if (stress == 4 && !textBox.activeSelf && !(turn >= 10) && !inText) {
			GameObject.Find("Checklist DDOL").GetComponent<ChecklistLogic>().GetTime();
			SceneManager.LoadScene(1);
		}
        if (inText && !textBox.activeSelf)
            inText= false;
        Mathf.Clamp(stress, 0, 4);
        stressLevels[stress].SetActive(true);
        if (stress == 2)
            angerSprite1.SetActive(true);
        if (stress == 4)
            angerSprite2.SetActive(true);
    }

    public void GoodOption(int meterLimit) {
        DisableButtons();
        inText = true;
        turn += 2;
        Mathf.Clamp(turn, 0, 10);
        speaker.Speak(textOptions[turn]);
        if (meterLimit!=0 && meterLimit>stress)
            stress = meterLimit;
    }

	public void BadOption() {
        DisableButtons();
		inText = true;
		turn ++;
		speaker.Speak(textOptions[turn]);
        turn ++;
        stress++;
	}

    public void DisableButtons() {
        foreach(GameObject x in buttons) {
            x.SetActive(false);
        }
    }

    public void ActivateButtons() {
		buttons[Mathf.FloorToInt(turn / 2)].SetActive(true);
	}
}
