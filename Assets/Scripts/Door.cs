using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    Transform doorCover;
    bool opening = false;
    public bool isOpen = false;
    public bool isDoorUp = true;
    
    // Start is called before the first frame update
    void Start()
    {
        BaseStart();
        doorCover = transform.GetChild(0);
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
        Debug.Log(isOpen);
        if (!isOpen) {
            if (isDoorUp) {
                // TODO check interaction condition
                if (gameObject.name.Equals("l0doorsup")) {
                    Debug.Log(player.carrying);
                    if (!player.carrying || !player.carrying.name.Equals("keycardtolevel1")) return;
                    else {
                        player.carrying.UseCarriable();
                        player.currentInteractable = this;
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
        }
    }

    public override void ResetState()
    {
        doorCover.localPosition = Vector3.zero;
        opening = false;
        isOpen = false;
    }
}
