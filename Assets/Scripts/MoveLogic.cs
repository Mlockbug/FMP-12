using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveLogic : MonoBehaviour
{
    public GameObject mainUI;
    public GameObject moveUI;

    public void LoadRoom(int room)
    {
        SceneManager.LoadScene(room);
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

    public void PlayPoker()
    {
        SceneManager.LoadScene(4);
    }
}