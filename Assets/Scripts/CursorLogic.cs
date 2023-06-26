using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CursorLogic : MonoBehaviour {
	int count;
	public Transform cursor;
	public Animator myAnim;
	GameObject thingToPickup;
	bool send;
	void Start() {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Confined;

		foreach (GameObject x in GameObject.FindGameObjectsWithTag("Cursor"))
			count++;
		if (count > 1 && this.gameObject.name == "Cursor")
			Destroy(this.gameObject);
		else {
			DontDestroyOnLoad(this);
			this.name = "Cursor DDOL";
			count = 0;
		}
	}

	void Update() {
		Mouse.current.WarpCursorPosition(new Vector2(1,1));
		cursor.position = FindObjectOfType<Camera>().ScreenToWorldPoint(Input.mousePosition);
		cursor.position = new Vector3(cursor.position.x + 0.1f, cursor.position.y - 0.15f, 0f);

		//I made this at about 10pm, it probably shouldn't work, but it does, and i will not be changing it as a result
		if (Input.GetMouseButtonDown(0)) {
			myAnim.SetTrigger("click");
			send = true;
		}
		if (Input.GetMouseButtonUp(0)) {
			thingToPickup = null;
			send = false;
		}
		if (SceneManager.GetActiveScene().buildIndex == 11 && thingToPickup!= null && send) {
			GameObject.Find("Cleaning Manager").GetComponent<CleaningLogic>().Pickup(thingToPickup);
			thingToPickup = null;
		}
	}
	void OnTriggerStay2D(Collider2D collision) {
		if (thingToPickup == null)
			thingToPickup = collision.gameObject;
	}
	void OnTriggerExit2D(Collider2D collision) {
		thingToPickup = null;
	}

	public void ClearPickup() {
		thingToPickup = null;
	}
}
