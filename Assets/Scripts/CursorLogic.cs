using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

	public Vector2 mousePos;

	bool hasBeenPressed = false;

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

	Vector2 joystickInput;

	public void Point(InputAction.CallbackContext ctx) {
		joystickInput = ctx.ReadValue<Vector2>();
	}

	void Update() {
		mousePos += (joystickInput * 400 * Time.deltaTime);

		mousePos = new Vector2(Mathf.Clamp(mousePos.x, 0f, Screen.width), Mathf.Clamp(mousePos.y, 0, Screen.height));
		Mouse.current.WarpCursorPosition(mousePos);

		cursor.position = FindObjectOfType<Camera>().ScreenToWorldPoint(mousePos);
		cursor.position = new Vector3(cursor.position.x + 0.1f, cursor.position.y - 0.15f, 0f);

		if (SceneManager.GetActiveScene().buildIndex == 11 && thingToPickup!= null && send) {
			GameObject.Find("Cleaning Manager").GetComponent<CleaningLogic>().Pickup(thingToPickup);
			thingToPickup = null;
			send = false;
		}


		if ((Mouse.current.leftButton.isPressed || Keyboard.current.xKey.isPressed)&& !hasBeenPressed)
		{
			hasBeenPressed = true;

			myAnim.SetTrigger("click");
			send = true;

			RaycastHit hit;

			if (Physics.Raycast(Camera.main.ScreenPointToRay(mousePos), out hit))
			{
				if (hit.transform.GetComponent<Button>().interactable)
					hit.transform.GetComponent<Button>().onClick?.Invoke();
			}
		}	

		if (hasBeenPressed && !(Mouse.current.leftButton.isPressed || Keyboard.current.xKey.isPressed))
        {
			hasBeenPressed = false;
			thingToPickup = null;
			send = false;
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
