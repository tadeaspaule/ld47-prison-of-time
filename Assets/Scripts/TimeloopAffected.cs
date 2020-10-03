using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeloopAffected : MonoBehaviour
{
    // original values to revert to
    Vector3 oPosition;
    Quaternion oRotation;
    Transform oParent;
    Sprite oSprite;
    Color oColor;
    
    SpriteRenderer sr;
    Interactable interactable;
    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        gm.AddTimeloopAffected(this);
        oPosition = new Vector3(transform.localPosition.x,transform.localPosition.y,transform.localPosition.z);
        Vector3 oRotationVect = transform.localRotation.eulerAngles;
        oRotation = Quaternion.Euler(oRotationVect.x,oRotationVect.y,oRotationVect.z);
        oParent = transform.parent;
        sr = GetComponent<SpriteRenderer>();
        if (sr) {
            oSprite = sr.sprite;
            oColor = sr.color;
        }
        interactable = GetComponent<Interactable>();
    }

    public void Revert()
    {
        gameObject.SetActive(true);
        transform.localPosition = oPosition;
        transform.localRotation = oRotation;
        transform.parent = oParent;
        if (sr) {
            sr.sprite = oSprite;
            sr.color = oColor;
        }
        if (interactable) interactable.ResetState();
    }
}
