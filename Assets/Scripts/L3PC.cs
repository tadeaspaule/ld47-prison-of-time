using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L3PC : Interactable
{
    L3Keypad keypad;
    void Start()
    {
        BaseStart();
        foreach (string n in partNames) neededParts.Add(n);
        keypad = FindObjectOfType<L3Keypad>();
    }

    void Update()
    {
        
    }

    string[] partNames = new string[]{"PCPART1","PCPART2"};
    List<string> neededParts = new List<string>();

    public override void Interact()
    {
        if (player.GetGameData("sawl3keypad") == null) return;
        if (player.GetGameData("foundl3pc") == null) {
            gm.ShowTextChain(new string[] {
                "o ye found pc",
                "should look around the scrap piles for parts to repair"
            });
            player.UpdateGameData("foundl3pc","true");
            player.currentInteractables.Remove(this);
        }
        else if (player.carrying && player.carrying.tag.Equals("pcPart")) {
            neededParts.Remove(player.carrying.name);
            player.carrying.UseCarriable();
            if (neededParts.Count == 0) {
                gm.ShowText($"we're in code is {keypad.correctCode}");
            }
            else {
                gm.ShowText($"I still need {string.Join(" and ",neededParts)}");
            }
        }
    }
    public override void ResetState()
    {
        neededParts.Clear();
        foreach (string n in partNames) neededParts.Add(n);
    }
    public override bool IsValidInteractable() {return player.GetGameData("sawl3keypad") != null;}
}
