using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    Transform doorCover;
    bool opening = false;
    public bool isOpen = false;
    public bool isDoorUp = true;
    bool hasBeenUnlocked = false;
    L3Keypad l3Keypad;
    Scanner[] scanners;
    
    // Start is called before the first frame update
    void Start()
    {
        scanners = FindObjectsOfType<Scanner>();
        BaseStart();
        doorCover = transform.GetChild(0);
        l3Keypad = FindObjectOfType<L3Keypad>();
    }

    // Update is called once per frame
    void Update()
    {
        if (opening) {
            doorCover.localPosition += Vector3.down * Time.deltaTime;
            if (doorCover.localPosition.y < -1f) {
                opening = false;
                isOpen = true;
            }
        }
    }
    public override void Interact()
    {
        if (!isOpen) {
            if (isDoorUp) {
                if (!hasBeenUnlocked) {
                    // door unlock interaction - any special things that need to happen
                    if (!CanOpen()) return;
                    hasBeenUnlocked = true;
                    if (gameObject.name.Equals("l0doorsup")) {
                        Debug.Log(player.carrying);
                        if (!player.carrying || !player.carrying.name.Contains("keycard")) return;
                        else {
                            player.carrying.UseCarriable();
                        }
                    }
                }
                opening = true;
            }
            else opening = true; // just open doors down, no conditions needed
        }
        else {
            // interact with open door, go up/down
            if (isDoorUp) gm.GoUp();
            else gm.GoDown();
            CloseDoor();
        }
    }

    bool CanOpen()
    {
        if (gameObject.name.Equals("l0doorsup")) {
            return (player.carrying && player.carrying.name.Contains("keycard"));
        }
        else if (gameObject.name.Equals("l1doorsup")) {
            foreach (Scanner s in scanners) if (!s.scannerHappy) return false;
            return true;
        }
        else if (gameObject.name.Equals("l2doorsup")) {
            return !gm.lockdown;
        }
        else if (gameObject.name.Equals("l3doorsup")) {
            return l3Keypad.keypadOpened;
        }
        return false;
    }

    void CloseDoor()
    {
        doorCover.localPosition = Vector3.zero;
        opening = false;
        isOpen = false;
    }

    public override void ResetState()
    {
        CloseDoor();
        hasBeenUnlocked = false;
    }
    public override bool IsValidInteractable() {Debug.Log(CanOpen());return isOpen || !isDoorUp || hasBeenUnlocked || CanOpen();}
}
