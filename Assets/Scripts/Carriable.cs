using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carriable : Interactable
{    
    void Start()
    {
        BaseStart();
        priorityInteractable = true;
    }

    public override void Interact()
    {
        player.carrying = this;
        player.currentInteractables.Remove(this);
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;
        transform.rotation = Quaternion.identity;
        gm.ShowText($"picked up {gameObject.name}");
    }

    public override void ResetState()
    {
        player.carrying = null;
    }

    public void UseCarriable()
    {
        transform.position = new Vector3(50f,50f,0f); // to hide it
        player.carrying = null;
    }
    public override bool IsValidInteractable() {return true;}
}
