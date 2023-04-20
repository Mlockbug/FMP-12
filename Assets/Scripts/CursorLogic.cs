using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLogic : MonoBehaviour
{
    int count;
    public Transform cursor;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        foreach (GameObject x in GameObject.FindGameObjectsWithTag("Cursor"))
            count++;
        if (count > 1 && this.gameObject.name == "Cursor")
            Destroy(this.gameObject);
        else
        {
            DontDestroyOnLoad(this);
            this.name = "Cursor DDOL";
            count = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        cursor.position = FindObjectOfType<Camera>().ScreenToWorldPoint(Input.mousePosition);
        cursor.position = new Vector3(cursor.position.x, cursor.position.y, 0f);
    }
}
