using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Draggable : Interactable
{    
    Transform oParent;

    protected void DraggableSetup()
    {
        BaseStart();
        priorityInteractable = true;
        oParent = transform.parent;
    }

    protected void BasicDraggableInteract()
    {
        player.dragging = this;
        player.currentInteractables.Remove(this);
        transform.parent = player.transform;
        // transform.localPosition = Vector3.zero;
        // transform.rotation = Quaternion.identity;
        gm.ShowText($"dragging {gameObject.name}");
    }
    public override void Interact()
    {
        BasicDraggableInteract();
    }
    public override void ResetState()
    {
        player.dragging = null;
    }
    public override bool IsValidInteractable() {return true;}

    public void LetGo()
    {
        transform.parent = oParent;
        player.dragging = null;
        // player.currentInteractables.Remove(this);
    }
}
