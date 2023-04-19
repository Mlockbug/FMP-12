using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChecklistLogic : MonoBehaviour
{
    public bool pokerDone;
    int count;
    void Start()
    {
        foreach (GameObject x in GameObject.FindGameObjectsWithTag("Checklist"))
            count++;
        if (count > 1 && this.gameObject.name == "Checklist")
            Destroy(this.gameObject);
        else{
            DontDestroyOnLoad(this);
            this.name = "Checklist DDOL";
        }
    }
}
