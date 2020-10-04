using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected GameManager gm;
    protected Player player;
    public abstract void Interact();
    public abstract void ResetState(); // called on timeloop trigger
    public bool priorityInteractable = false;
    public string hintName;

    protected void BaseStart(string hintName)
    {
        gm = FindObjectOfType<GameManager>();
        player = FindObjectOfType<Player>();
        this.hintName = hintName;
    }

    public abstract bool IsValidInteractable();
}
