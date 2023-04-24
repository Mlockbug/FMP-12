using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PokerLogic : MonoBehaviour {
	[Header("Rounds")]
	public GameObject[] round1Cards;
	public GameObject[] round2Cards;
	public GameObject[] round3Cards;
	public GameObject[,] cards = new GameObject[3, 5];
	public GameObject backOfEdwardHand;
	int round = 0;
	int pot = 0;
	int maxBet = 0;
	int money = 2000;
	int stage = 0;
	int edwardBet;
	bool goneAllIn = false;
	bool waiting;
	bool inactive = false;

	[Header("Values Display")]
	public Text moneyDisplay;
	public Text potDisplay;

	[Header("UI")]
	public Text announcementText;
	public GameObject betButtons;
	public GameObject raiseMenu;
	public GameObject textBox;
	public Button foldButton;
	public Button allInButton;
	public Button raiseButton;
	bool canFold = false;
	bool canAllIn = false;
	bool canRaise = true;
	bool inText = false;

	[Header("Dialogue")]
	Dialogue round1Dialogue;
	Dialogue round2Dialogue;
	Dialogue round3Dialogue;
	Dialogue cannotDo;
	Dialogue giveChips;

	void Start() {
		SortArray(round1Cards, cards, 0); SortArray(round2Cards, cards, 1); SortArray(round3Cards, cards, 2);
		round1Dialogue = GameObject.Find("Text 1").GetComponent<Dialogue>();
		round2Dialogue = GameObject.Find("Text 2").GetComponent<Dialogue>();
		round3Dialogue = GameObject.Find("Text 3").GetComponent<Dialogue>();
		cannotDo = GameObject.Find("starting diag").GetComponent<Dialogue>();
		giveChips = GameObject.Find("Give Chips").GetComponent<Dialogue>();
	}

	void Update() {
		moneyDisplay.text = "Chips: " + money.ToString();
		potDisplay.text = "Pot: " + pot.ToString();
		foldButton.interactable = canFold;
		allInButton.interactable = canAllIn;
		raiseButton.interactable = canRaise;
		edwardBet = 50 * (3 - round);
		if (round == 2)
			edwardBet = 100;

		if (round == 3) {
			GameObject.Find("Checklist DDOL").GetComponent<ChecklistLogic>().pokerDone = true;
			SceneManager.LoadScene(2);
		}

		if (inText && !textBox.activeSelf) {
			stage++;
			inText = false;
		}

		if (!waiting && !inactive && !inText) {
			switch (stage) {
				case 0:
					if (money != 0 && !textBox.activeSelf) {
						maxBet = 0;
						if (round != 0)
							Disable(round - 1);
						canAllIn = false;
						canFold = false;
						goneAllIn = false;
						StartCoroutine(Wait());
					}
					else if (money == 0 && !textBox.activeSelf)
						money = 750;
					break;
				case 1:
					pot += 85;
					money -= 30;
					StartCoroutine(Wait());
					StartCoroutine(Announce("Entry + blinds"));
					break;
				case 2:
					cards[round, 0].SetActive(true);
					backOfEdwardHand.SetActive(true);
					StartCoroutine(Wait());
					StartCoroutine(Announce("Dealing hand"));
					break;
				case 3:
					if (round == 0)
						GameObject.Find("Dialogue Manager").GetComponent<DialogueLogic>().Speak(cannotDo);
					inText = true;
					break;
				case 4:
					betButtons.SetActive(true);
					inactive = true;
					break;
				case 5:
					if (maxBet >= 50) {
						Call(false);
						StartCoroutine(Announce("Edward bets " + maxBet.ToString()));
					}
					else {
						Bet(edwardBet.ToString() + "noo");
						StartCoroutine(Announce("Edward bets " + edwardBet.ToString()));
					}
					break;
				case 6:
					cards[round, 1].SetActive(true);
					StartCoroutine(Wait());
					StartCoroutine(Announce("Flop"));
					break;
				case 7:
					maxBet = 0;
					betButtons.SetActive(true);
					inactive = true;
					break;
				case 8:
					if (maxBet >= edwardBet) {
						Call(false);
						StartCoroutine(Announce("Edward bets " + maxBet.ToString()));
					}
					else {
						Bet(edwardBet.ToString() + "noo");
						StartCoroutine(Announce("Edward bets " + edwardBet.ToString()));
					}
					break;
				case 9:
					cards[round, 2].SetActive(true);
					StartCoroutine(Wait());
					StartCoroutine(Announce("Turn"));
					canFold = true;
					canAllIn = true;
					break;
				case 10:
					maxBet = 0;
					betButtons.SetActive(true);
					inactive = true;
					break;
				case 11:
					if (maxBet >= edwardBet) {
						Call(false);
						StartCoroutine(Announce("Edward bets " + maxBet.ToString()));
						if (goneAllIn)
							cards[round, 4].SetActive(true);
					}
					else {
						Bet(edwardBet.ToString() + "noo");
						StartCoroutine(Announce("Edward bets " + edwardBet.ToString()));
					}
					break;
				case 12:
					cards[round, 3].SetActive(true);
					StartCoroutine(Wait());
					StartCoroutine(Announce("River"));
					canFold = true;
					canAllIn = true;
					if (goneAllIn) {
						Debug.Log("ASDAS00");
						stage = 14;
					}
					break;
				case 13:
					maxBet = 0;
					betButtons.SetActive(true);
					inactive = true;
					break;
				case 14:
					if (round == 0 && goneAllIn) {
						StartCoroutine(Announce("Edward folds"));
						StartCoroutine(Wait());
					}
					else if (maxBet >= 150) {
						Call(false);
						StartCoroutine(Announce("Edward bets " + maxBet.ToString()));
						if (goneAllIn)
							cards[round, 4].SetActive(true);
					}
					else {
						Bet(edwardBet.ToString() + "noo");
						StartCoroutine(Announce("Edward bets " + edwardBet.ToString()));
					}
					break;
				case 15:
					if (round == 0) {
						StartCoroutine(Announce("You won " + pot));
						money += pot;
					}
					else {
						StartCoroutine(Announce("Edward won " + pot));
					}
					if (!(round == 0 && goneAllIn))
						cards[round, 4].SetActive(true);
					pot = 0;
					StartCoroutine(Wait());
					break;
				case 16:
					betButtons.SetActive(false);
					raiseMenu.SetActive(false);
					switch (round) {
						case 0:
							GameObject.Find("Dialogue Manager").GetComponent<DialogueLogic>().Speak(round1Dialogue);
							break;
						case 1:
							GameObject.Find("Dialogue Manager").GetComponent<DialogueLogic>().Speak(round2Dialogue);
							break;
						case 2:
							GameObject.Find("Dialogue Manager").GetComponent<DialogueLogic>().Speak(round3Dialogue);
							break;
					}
					inText = true;
					break;
				case 17:
					if (round == 1 && goneAllIn)
						GameObject.Find("Dialogue Manager").GetComponent<DialogueLogic>().Speak(giveChips);
					backOfEdwardHand.SetActive(false);
					round++;
					stage = 0;
					break;
			}
		}
	}

	public void ShowRaiseMenu() {
		betButtons.SetActive(false);
		raiseMenu.SetActive(true);
	}
	public void HideRaiseMenu() {
		betButtons.SetActive(true);
		raiseMenu.SetActive(false);
	}

	public void SortArray(GameObject[] toSort, GameObject[,] sort, int row) {
		int pass = 0;
		foreach (GameObject x in toSort) {
			sort[row, pass] = x;
			pass++;
		}
	}
	public void Call(bool player) {
		if (player)
			money -= maxBet;
		betButtons.SetActive(false);
		pot += maxBet;
		StartCoroutine(Wait());
	}
	public void Bet(string input) {
		int bet = System.Convert.ToInt32(input.Substring(0, 3));
		if (bet > maxBet) {
			maxBet = bet;
			if (input.Substring(3, 3) == "yes")
				money -= maxBet;
			pot += maxBet;
			StartCoroutine(Wait());
			StartCoroutine(Announce("You bet " + System.Convert.ToInt32(input.Substring(0, 3)).ToString()));
			raiseMenu.SetActive(false);
		}
	}
	public void AllIn() {
		if (canAllIn) {
			pot += money;
			maxBet = money;
			money = 0;
			goneAllIn = true;
			betButtons.SetActive(false);
			StartCoroutine(Announce("You went all in"));
			StartCoroutine(Wait());
		}
	}
	public void Fold() {
		StartCoroutine(Announce("You chose to fold, Edward wins " + pot.ToString()));
		pot = 0;
		stage = 16;
		inactive = false;
	}

	IEnumerator Wait() {
		waiting = true;
		yield return new WaitForSeconds(2);
		stage++;
		waiting = false;
		inactive = false;
		StopCoroutine(Wait());
	}

	private void Disable(int row) {
		for (int i = 0; i <= 4; i++)
			cards[row, i].SetActive(false);
	}

	IEnumerator Announce(string message) {
		announcementText.text = message;
		yield return new WaitForSeconds(2);
		announcementText.text = "";
		StopCoroutine(Announce(message));
	}
}
