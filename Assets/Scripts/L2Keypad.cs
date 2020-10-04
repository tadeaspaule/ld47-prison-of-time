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
        BaseStart("Keypad");
    }

    void Update()
    {
        if (gm.paused) return;
        if (!keypadUI.activeSelf) return;
        if (Input.GetKeyDown(KeyCode.Q)) {
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
            "Damn! This door will only let me through if I'm in my cell, but then I can't go through...",
            "I need to fake my presence somehow, I should look around for something that does that.",
            "Ah! The dead guard at the guard office! Gruesome as it feels, I think I'll have to use his body for this."
        });
        gm.blockInput = true;
        // allowKeypadInput = true;
        player.UpdateGameData("sawl2keypad","true");
        player.currentInteractables.Remove(this);
    }
    public override void ResetState()
    {
        
    }
    public override bool IsValidInteractable() {return gm.lockdown;}
}
