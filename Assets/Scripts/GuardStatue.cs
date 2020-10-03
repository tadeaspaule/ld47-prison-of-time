using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardStatue : Draggable
{
    void Start()
    {
        DraggableSetup();
    }

    int presenceMaskerPartsAdded = 0;
    public static int maxMaskerParts = 3;
    public override void Interact()
    {
        if (player.carrying && player.carrying.tag.Equals("presenceMaskerPart")) {
            presenceMaskerPartsAdded++;
            player.carrying.UseCarriable();
            if (presenceMaskerPartsAdded == maxMaskerParts) {

            }
        }
        else if (!player.carrying && !player.dragging && presenceMaskerPartsAdded == maxMaskerParts) {
            // this will disable the lockdown
            gm.ShowText("great lockdown disabled yay");
        }
    }
    public override void ResetState()
    {
        player.dragging = null;
        presenceMaskerPartsAdded = 0;
    }
}
