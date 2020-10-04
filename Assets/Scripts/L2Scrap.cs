using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2Scrap : ScrapPile
{
    // Start is called before the first frame update
    void Start()
    {
        BaseStart("Pile of scrap");
        ScrapPileSetup();
    }

    public override void Interact()
    {
        if (player.GetGameData("sawl2keypad") == null) gm.ShowText("A pile of scrap, bits and pieces of various technology. I wonder why that's here.");
        else base.Interact();
    }

    public override bool IsValidInteractable()
    {
        return BasicIsValidInteractable();
        // string v = player.GetGameData("sawl2keypad");
        // return (BasicIsValidInteractable() && v != null && v.Equals("true"));
    }
}
