using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveLogic : MonoBehaviour
{
    public GameObject mainUI;
    public GameObject moveUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadRoom(int room)
    {
        SceneManager.LoadScene(room - 1);
    }

    public void ShowMoveUI()
    {
        mainUI.SetActive(false);
        moveUI.SetActive(true);
    }

    public void ShowMainUI()
    {
        mainUI.SetActive(true);
        moveUI.SetActive(false);
    }

    public void Test(Dialogue text)
	{
        Debug.Log(text.text[0]);
	}
}
