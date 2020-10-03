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
        scavangeScreen.OpenScreen(items);
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
