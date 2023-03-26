using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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

    public void Speak(Dialogue text){
        continueText = true;
		mainUI.SetActive(false);
		textBox.SetActive(true);

        if (fullDialogue != null)
		    fullDialogue.Clear();

        foreach (string x in text.text)
            fullDialogue.Enqueue(x);

        while (fullDialogue.Count > 0){
            if (cont.activeSelf == true && Input.GetKeyDown(KeyCode.Return)){
                cont.SetActive(false);
                continueText = true;
            }

            if (continueText){
                textDisplay.text = "";
                diagString = fullDialogue.Dequeue();
                if (diagString.Contains("NAME-"))
                    nameDisplay.text = diagString.Split('-')[1];
                else
                    StartCoroutine(ReadText());
            }
        }
    }

    IEnumerator ReadText()
    {
        continueText = false;
		foreach (char x in diagString)
		{
			textDisplay.text += x;
			if (x == ' ')
                yield return new WaitForSeconds(0.1f);
		}
        cont.SetActive(true);
    }
}
