using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueLogic : MonoBehaviour
{
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

    [Header("Minigame completed dialogue")]
    public string[] pokerCompleted;
    Dialogue pokerText;

	private void Start(){
        createDialogue();
        checklist = GameObject.Find("Checklist DDOL").GetComponent<ChecklistLogic>();
        if (startingText.Length != 0){
            Dialogue startingDiag = gameObject.AddComponent<Dialogue>();
            startingDiag.text = startingText;
            Speak(startingDiag);
        }
	}

	private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Semicolon))
            SceneManager.LoadScene(0);

        if (fullDialogue.Count > 0){
            if (cont.activeSelf == true && Input.GetKeyDown(KeyCode.Return)){
                cont.SetActive(false);
                continueText = true;
            }

            if (!continueText && Input.GetKeyDown(KeyCode.Return)){
                textDisplay.text = diagString;
                cont.SetActive(true);
			}

            if (continueText)
            {
                textDisplay.text = "";
                diagString = fullDialogue.Dequeue();
                if (diagString.Contains("NAME-"))
                    nameDisplay.text = diagString.Split('-')[1];
                else if (diagString == "END") {
                    cont.SetActive(false);
                    textBox.SetActive(false);
                    mainUI.SetActive(true);
                }
                else
                    StartCoroutine(ReadText());
            }
        }
    }

	public void Speak(Dialogue text)
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 2 when checklist.pokerDone:
                text = pokerText;
                break;
        }

        continueText = true;
		mainUI.SetActive(false);
		textBox.SetActive(true);

        if (fullDialogue != null)
		    fullDialogue.Clear();

        foreach (string x in text.text)
            fullDialogue.Enqueue(x);
    }

    IEnumerator ReadText()
    {
        continueText = false;
		foreach (char x in diagString){
            if (cont.activeSelf == false){
                textDisplay.text += x;
                if (x != ' ')
                    yield return new WaitForSeconds(0.2f);
            }
		}
        cont.SetActive(true);
    }

    void createDialogue()
	{
        //doing this here to not clutter start
        pokerText = gameObject.AddComponent<Dialogue>(); pokerText.text = pokerCompleted;
	}
}
