using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;


public class CursorLogic : MonoBehaviour {
	int count;
	public Transform cursor;
	public Animator myAnim;
	GameObject thingToPickup;
	SpriteRenderer spriteRenderer;

	bool send;

	Vector2 mousePos;

	void Start() {
		mousePos = new Vector2(Screen.width / 2, Screen.height / 2);

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

	public void Click (InputAction.CallbackContext ctx) { 
		if (ctx.performed) {
			myAnim.SetTrigger("click");
			send = true;
		}

		if (ctx.canceled) {
			thingToPickup = null;
			send = false;
		}
	}

	Vector2 joystickInput;

	public void Point(InputAction.CallbackContext ctx) {
		joystickInput = ctx.ReadValue<Vector2>();
	}

	void Update() {
		mousePos += joystickInput;

		mousePos = new Vector2(Mathf.Clamp(mousePos.x, 0f, Screen.width), Mathf.Clamp(mousePos.y, 0, Screen.height));
		Mouse.current.WarpCursorPosition(mousePos);

		cursor.position = FindObjectOfType<Camera>().ScreenToWorldPoint(mousePos);
		cursor.position = new Vector3(cursor.position.x + 0.1f, cursor.position.y - 0.15f, 0f);

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
