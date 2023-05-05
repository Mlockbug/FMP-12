using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorLogic : MonoBehaviour {
	int count;
	public Transform cursor;
	public Animator myAnim;
	GameObject thingToPickup;
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
		cursor.position = FindObjectOfType<Camera>().ScreenToWorldPoint(Input.mousePosition);
		cursor.position = new Vector3(cursor.position.x + 0.1f, cursor.position.y - 0.15f, 0f);
		if (Input.GetMouseButtonDown(0))
			myAnim.SetTrigger("click");
		if (SceneManager.GetActiveScene().buildIndex == 9) {
			GameObject.Find("Cleaning Manager").GetComponent<CleaningLogic>().Pickup(thingToPickup);
		}
	}
	void OnTriggerEnter2D(Collider2D collision) {
		thingToPickup = collision.gameObject;
	}
	void OnTriggerExit2D(Collider2D collision) {
		thingToPickup = null;
	}
}
