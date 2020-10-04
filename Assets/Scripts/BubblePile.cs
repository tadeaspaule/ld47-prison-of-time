using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePile : Interactable
{
    bool tookBubble = false;
    public string toolName;
    void Start()
    {
        BaseStart("Pile of debris");
    }
    public override void Interact()
    {
        ResetState(); // kinda cheat since they get out of sync and it's unclear and annoying (though it makes sense)
        if (!tookBubble) {
            gm.ShowTextChain(new string[]{
                "Oh great! Another emergency Bubble Tool!"
            });
            gm.AddBubbleTool(toolName);
            tookBubble = true;
        }
        else {
            gm.ShowText("Looks like the only useful thing here was the Bubble tool");
        }
    }
    public override void ResetState()
    {
        tookBubble = !(player.placedBubbletools.Find(b => b.Equals(toolName)) == null && player.bubbleTools.Find(b => b.Equals(toolName)) == null);
    }
    public override bool IsValidInteractable() {return true;}
}
