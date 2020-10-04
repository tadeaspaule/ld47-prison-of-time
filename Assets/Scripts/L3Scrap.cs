using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L3Scrap : ScrapPile
{
    // Start is called before the first frame update
    void Start()
    {
        BaseStart("Pile of scrap");
        ScrapPileSetup();
    }

    public override void Interact()
    {
        if (player.GetGameData("foundl3pc") != null) base.Interact();
        else gm.ShowText("Nothing in this pile that would help get the passcode.");
    }

    public override bool IsValidInteractable()
    {
        return (BasicIsValidInteractable() && player.GetGameData("sawl3keypad") != null);
    }
}
