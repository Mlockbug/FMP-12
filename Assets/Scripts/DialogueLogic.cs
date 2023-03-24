using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueLogic : MonoBehaviour
{
    public GameObject textBox;
    public GameObject mainUI;
    public Text textDisplay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Test(Dialogue text)
    {
        mainUI.SetActive(false);
        textBox.SetActive(true);
        textDisplay.text = text.text[0];
    }
}
