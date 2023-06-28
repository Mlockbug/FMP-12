using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpeningCutscene : MonoBehaviour
{
    public RawImage blackScreen;
    Color screenColor = Color.white;
    float alpha = 1;
    public GameObject dialogueManager;
    bool waitedLongEnough;
    void Start()
    {
        StartCoroutine(WaitForStart());
    }

    // Update is called once per frame
    void Update()
    {
        if (waitedLongEnough && alpha> 0) {
            alpha -= 0.1f;
        }
        else if (waitedLongEnough && alpha <= 0)
			dialogueManager.SetActive(true);
		Mathf.Clamp(alpha, 0.0f, 1.0f);
        screenColor.a= alpha;
        blackScreen.color = screenColor;
    }

    IEnumerator WaitForStart() {
        yield return new WaitForSeconds(10f);
        waitedLongEnough= true;
    }
}
