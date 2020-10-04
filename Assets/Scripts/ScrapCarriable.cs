using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapCarriable : Carriable
{    
    public ScrapItem item;
    public SpriteRenderer sr;
    
    void Start()
    {
        BaseStart("");
    }

    public void SetupSC(GameManager gm, Player player, ScrapItem inItem)
    {
        this.gm = gm;
        this.player = player;
        item = inItem;
        itemName = inItem.name;
        sr.sprite = item.GetSprite();
    }

    public override bool IsValidInteractable() {return false;}
}
