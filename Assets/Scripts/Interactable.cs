using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected GameManager gm;
    protected Player player;
    public abstract void Interact();
    public abstract void ResetState(); // called on timeloop trigger

    protected void BaseStart()
    {
        gm = FindObjectOfType<GameManager>();
        player = FindObjectOfType<Player>();
    }
}
