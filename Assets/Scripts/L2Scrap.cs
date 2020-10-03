using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2Scrap : ScrapPile
{
    // Start is called before the first frame update
    void Start()
    {
        BaseStart();
        ScrapPileSetup();
    }

    public override bool IsValidInteractable()
    {
        string v = player.GetGameData("sawl2keypad");
        return (BasicIsValidInteractable() && v != null && v.Equals("true"));
    }
}
