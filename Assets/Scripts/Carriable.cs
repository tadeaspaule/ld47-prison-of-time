using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carriable : Interactable
{
    void Start()
    {
        BaseStart();
    }

    public override void Interact()
    {
        player.carrying = this;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    public override void ResetState()
    {
        player.carrying = null;
    }
}
