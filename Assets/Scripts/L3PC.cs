using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L3PC : Interactable
{
    L3Keypad keypad;
    SpriteRenderer sr;
    SpriteRenderer pcScreen;
    GameObject pcModel;
    Color onColor = new Color(0.427451f,0.7372549f,0.5333334f,0.6039216f);
    Color offColor = new Color(0.4f,0.4f,0.4f);
    void Start()
    {
        BaseStart("Pile of scrap");
        foreach (string n in partNames) neededParts.Add(n);
        keypad = FindObjectOfType<L3Keypad>();
        pcScreen = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
        pcModel = transform.GetChild(0).gameObject;
        sr = GetComponent<SpriteRenderer>();
    }

    string[] partNames = new string[]{"Fuel cell","Bundle of wires"};
    List<string> neededParts = new List<string>();

    public override void Interact()
    {
        if (player.GetGameData("sawl3keypad") == null) return;
        if (player.GetGameData("foundl3pc") == null) {
            gm.ShowTextChain(new string[] {
                "A computer! With this, getting that passcode will be a piece of cake!",
                "...",
                "Of course, it would have to have some power first. Great.",
                "Hopefully I can find a power source somewhere, as well as a way to hook it up."
            });
            pcModel.SetActive(true);
            hintName = "Computer";
            player.UpdateHintText();
            sr.enabled = false;
            pcScreen.color = offColor;
            player.UpdateGameData("foundl3pc","true");
            // player.currentInteractables.Remove(this);
        }
        else if (player.carrying && player.carrying.tag.Equals("pcPart")) {
            neededParts.Remove(player.carrying.name);
            player.carrying.UseCarriable();
            if (neededParts.Count == 0) {
                pcScreen.color = onColor;
                gm.ShowTextChain(new string[]{
                    "Done! And it's connected to their internal network as well, how convenient. Passcode, here I come!",
                    "...",
                    $"Ah, there we go! Looks like the passcode is {keypad.correctCode}. Let's get out of here!"
                });
            }
            else if (neededParts[0] == "Fuel cell") gm.ShowText($"I still need a power source");
            else gm.ShowText($"I still need a way to connect the fuel cell to the computer");
        }
        else if (neededParts.Count == 0) {
            gm.ShowText($"The passcode is {keypad.correctCode}");
        }
        else if (!player.carrying && !player.dragging) {
            if (neededParts[0] == "Fuel cell") gm.ShowText($"I still need a power source");
            else gm.ShowText($"I still need a way to connect the fuel cell to the computer");
        }
    }
    public override void ResetState()
    {
        neededParts.Clear();
        foreach (string n in partNames) neededParts.Add(n);
        pcScreen.color = offColor;
        pcModel.SetActive(false);
        sr.enabled = true;
    }
    public override bool IsValidInteractable() {return player.GetGameData("sawl3keypad") != null;}
}
