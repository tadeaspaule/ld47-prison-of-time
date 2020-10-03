using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapCarriable : Carriable
{    
    public ScrapItem item;
    public SpriteRenderer sr;
    
    void Start()
    {
        BaseStart();
        priorityInteractable = true;
    }

    public void SetupSC(GameManager gm, Player player, ScrapItem inItem)
    {
        this.gm = gm;
        this.player = player;
        item = inItem;
        sr.sprite = item.GetSprite();
    }

    public override bool IsValidInteractable() {return false;}
}
