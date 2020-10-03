using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class L2Keypad : Interactable
{
    public GameObject keypadUI;
    public TextMeshProUGUI keypadText;

    void Start()
    {
        BaseStart();
    }

    void Update()
    {
        if (gm.paused) return;
        if (!keypadUI.activeSelf) return;
        if (Input.GetKeyDown(KeyCode.E)) {
            keypadUI.SetActive(false);
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
        keypadUI.SetActive(true);
        gm.ShowTextChain(new string[] {
            "o no lockdown",
            "should look around the scrap piles"
        });
        gm.blockInput = true;
        // allowKeypadInput = true;
        player.UpdateGameData("sawl2keypad","true");
        player.currentInteractables.Remove(this);
    }
    public override void ResetState()
    {
        
    }
    public override bool IsValidInteractable() {return true;}

    // bool allowKeypadInput = false;
    
    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.tag.Equals("player")) {
    //         allowKeypadInput = true;
    //     }
    // }

    // void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.tag.Equals("player")) {
    //         allowKeypadInput = false;
    //     }
    // }
}
