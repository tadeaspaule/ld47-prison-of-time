using UnityEngine;

[System.Serializable]
public class ScrapItem
{
    public string name;
    public string description;

    public Sprite GetSprite()
    {
        return Resources.Load<Sprite>("ScrapSprites/whitesquare");
    }
}