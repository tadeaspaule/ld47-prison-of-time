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
        player.currentInteractable = null;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;
        transform.rotation = Quaternion.identity;
        gm.ShowText($"picked up {gameObject.name}");
    }

    public override void ResetState()
    {
        player.carrying = null;
    }
}
