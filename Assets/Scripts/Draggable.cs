using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : Interactable
{    
    Transform oParent;
    
    void Start()
    {
        BaseStart();
        priorityInteractable = true;
        oParent = transform.parent;
    }

    public override void Interact()
    {
        player.dragging = this;
        player.currentInteractables.Remove(this);
        transform.parent = player.transform;
        // transform.localPosition = Vector3.zero;
        // transform.rotation = Quaternion.identity;
        gm.ShowText($"dragging {gameObject.name}");
    }

    public override void ResetState()
    {
        player.carrying = null;
    }

    public void LetGo()
    {
        transform.parent = oParent;
        player.dragging = null;
        // player.currentInteractables.Remove(this);
    }
}
