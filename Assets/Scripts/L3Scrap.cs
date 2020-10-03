using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L3Scrap : ScrapPile
{
    // Start is called before the first frame update
    void Start()
    {
        BaseStart();
        ScrapPileSetup();
    }

    public override void Interact()
    {
        if (player.GetGameData("foundl3pc") != null) base.Interact();
        else gm.ShowText("nothing in this pile");
    }

    public override bool IsValidInteractable()
    {
        return (BasicIsValidInteractable() && player.GetGameData("sawl3keypad") != null);
    }
}
