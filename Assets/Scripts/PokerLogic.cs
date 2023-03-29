using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokerLogic : MonoBehaviour
{
    [Header("Rounds")]
    public GameObject[] round1Cards;
    public GameObject[] round2Cards;
    public GameObject[] round3Cards;
    public GameObject[,] cards;
    int round = 1;
    int pot = 0;
    int maxBet;
    int money = 2000;
    int stage = 0;

    [Header("Values Display")]
    public Text moneyDisplay;
    public Text potDisplay;

    [Header("UI")]
    public GameObject betButtons;
    public GameObject raiseMenu;
    public Button foldButton;
    public Button allInButton;
    public Button raiseButton;
    bool canFold = false;
    bool canAllIn = false;
    bool canRaise = true;

    [Header("Dialogue")]
    public string[] round1Text;
    public string[] round2Text;
    public string[] round3Text;

	void Start()
    {
        SortArray(round1Cards, cards, 0);
        SortArray(round2Cards, cards, 1);
        SortArray(round3Cards, cards, 2);
        Dialogue round1Dialogue= new Dialogue();  round1Dialogue.text = round1Text;
        Dialogue round2Dialogue= new Dialogue();  round2Dialogue.text = round2Text;
        Dialogue round3Dialogue= new Dialogue();  round3Dialogue.text = round3Text;
    }

    void Update()
    {
        moneyDisplay.text = "Chips: "+money.ToString();
        potDisplay.text = "Pot: "+pot.ToString();
        allInButton.interactable = canAllIn;
        raiseButton.interactable = canRaise;

        //need  to redo this switch case

        switch (round){
            case 1:
                round1Cards[0].SetActive(true);
                round2Cards[0].SetActive(false);
                round3Cards[0].SetActive(false);
                break;
            case 2:
				round1Cards[0].SetActive(false);
				round2Cards[0].SetActive(true);
				round3Cards[0].SetActive(false);
                break;
            case 3:
				round1Cards[0].SetActive(false);
				round2Cards[0].SetActive(false);
				round3Cards[0].SetActive(true);
                break;
        }
        if (maxBet == 150)
            canRaise = false;
    }

    public void Bet(int amount)
    {

    }
    public void ShowRaiseMenu(){
        betButtons.SetActive(false);
        raiseMenu.SetActive(true);
    }
    public void HideRaiseMenu(){
        betButtons.SetActive(true);
        raiseMenu.SetActive(false);
    }

    public void SortArray(GameObject[] toSort, GameObject[,] sort, int row)
    {
        int pass = 0;
        foreach(GameObject x in toSort)
        {
            sort[row, pass] = x;
        }
    }
}
