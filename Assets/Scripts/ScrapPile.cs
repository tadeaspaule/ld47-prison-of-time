using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScrapPile : Interactable
{
    public List<ScrapItem> items;
    List<ScrapItem> originalItems = new List<ScrapItem>();
    protected ScavangeScreen scavangeScreen;

    protected void ScrapPileSetup()
    {
        scavangeScreen = gm.scavangeScreen;
        foreach (ScrapItem i in items) originalItems.Add(i);
    }

    public override void Interact()
    {
        ResetState(); // kinda cheat but so that it's not annoying for players
        if (items.Count > 0) scavangeScreen.OpenScreen(items);
        else gm.ShowText("Only junk left in this pile");
    }

    public override void ResetState()
    {
        items = originalItems.FindAll(i => !gm.usedItems.Contains(i.name));
    }

    protected bool BasicIsValidInteractable()
    {
        return !player.dragging && !player.carrying;
    }
}
