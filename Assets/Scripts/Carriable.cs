﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carriable : Interactable
{    
    public bool destroyOnUse = true;
    protected string itemName;
    
    void Start()
    {
        BaseStart("");
        priorityInteractable = true;
    }

    public void ForceSetup(Player player, GameManager gameManager)
    {
        this.player = player;
        this.gm = gameManager;
    }

    public override void Interact()
    {
        player.carrying = this;
        player.currentInteractables.Remove(this);
        transform.parent = player.transform.GetChild(0);
        transform.localPosition = Vector3.right * 0.3f;
        transform.rotation = Quaternion.identity;
        // gm.ShowText($"picked up {gameObject.name}");
    }

    public override void ResetState()
    {
        player.carrying = null;
    }

    public void UseCarriable()
    {
        transform.position = new Vector3(50f,50f,0f); // to hide it
        gm.usedItems.Add(itemName);
        player.carrying = null;
        if (destroyOnUse) Destroy(gameObject);
    }
    public override bool IsValidInteractable() {return true;}
}
