using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScrapPile : Interactable
{
    public List<ScrapItem> items;
    protected ScavangeScreen scavangeScreen;

    protected void ScrapPileSetup()
    {
        // Debug.Log()
        scavangeScreen = gm.scavangeScreen;
    }

    public override void Interact()
    {
        if (items.Count > 0) scavangeScreen.OpenScreen(items);
        else gm.ShowText("nothing more in this pile");
    }

    public override void ResetState()
    {
        Debug.Log("scrap reset");
        // player.carrying = null;
    }

    protected bool BasicIsValidInteractable()
    {
        return !player.dragging && !player.carrying;
    }
}
