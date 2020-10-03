using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class L3Keypad : Interactable
{
    System.Random r = new System.Random();
    public L3KeypadUI keypadUI;
    public string correctCode = "9231012";
    public bool keypadOpened = false;

    void Start()
    {
        BaseStart();
        correctCode = "";
        for (int i = 0; i < 6; i++) correctCode += r.Next(10);
        Debug.Log(correctCode);
    }

    void Update()
    {
        if (gm.paused) return;
        if (!keypadUI.gameObject.activeSelf) return;
        if (Input.GetKeyDown(KeyCode.E)) {
            keypadUI.gameObject.SetActive(false);
            StartCoroutine(DelayedEnableInput());
        }
    }
    
    IEnumerator DelayedEnableInput()
    {
        yield return new WaitForEndOfFrame();
        gm.blockInput = false;
    }

    public override void Interact()
    {
        if (player.GetGameData("sawl3keypad") == null) {
            gm.ShowTextChain(new string[] {
                "o no no time to guess",
                "should look around the scrap piles for something"
            });
            player.UpdateGameData("sawl3keypad","true");
        }
        keypadUI.OpenKeypad(correctCode);
        gm.blockInput = true;
        // allowKeypadInput = true;
        player.currentInteractables.Remove(this);
    }
    public override void ResetState()
    {
        correctCode = "";
        for (int i = 0; i < 6; i++) correctCode += r.Next(10);
        keypadOpened = false;
    }
    public override bool IsValidInteractable() {return true;}

}
