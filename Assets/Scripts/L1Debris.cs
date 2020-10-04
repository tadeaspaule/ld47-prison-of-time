using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1Debris : Interactable
{
    string[] initialText;
    int bubblesInPile = 2;

    void Start()
    {
        BaseStart("Pile of debris");
        initialText = new string[]{
            "A pile of debris from whatever happened to these guards... Maybe it contains something useful?",
            "...",
            "I can't believe I'm doing this, what if I end up like them? But I have to get out of here!",
            "...",
            "Aha! Two emergency Time Bubble tools! This will definitely be useful",
            "(press Space to deploy Time Bubbles)",
            "(Time Bubbles free everything inside them from the prison's time loop)"
        };
    }
    public override void Interact()
    {
        ResetState(); // kinda cheat since they get out of sync and it's unclear and annoying (though it makes sense)
        if (bubblesInPile > 0) {
            initialText[4] = bubblesInPile > 1  ? "Aha! Two emergency Time Bubble tools! These will definitely be useful" : "Aha! An Time Bubble tool! This will definitely be useful";
            gm.ShowTextChain(initialText);
            // potentially gets rid of the tutorial information so you only read it once
            string[] t = new string[5];
            for (int i = 0; i < 5; i++) t[i] = initialText[i];
            initialText = t;
            for (int i = 0; i < bubblesInPile; i++) player.bubbleTools.Add($"l1debrisbubble{i}");
            bubblesInPile = 0;
        }
        else {
            gm.ShowText("Looks like the only useful thing here were the Bubble tools");
        }
    }
    public override void ResetState()
    {
        Debug.Log("reset debris");
        bubblesInPile = 0;
        for (int i = 0; i < 2; i++) {
            if (player.placedBubbletools.Find(b => b.Equals($"l1debrisbubble{i}")) == null && player.bubbleTools.Find(b => b.Equals($"l1debrisbubble{i}")) == null) {
                bubblesInPile++;
            }
        }
        Debug.Log(bubblesInPile);
    }
    public override bool IsValidInteractable() {return true;}
}
