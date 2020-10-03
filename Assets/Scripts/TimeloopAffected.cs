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
    Collider2D collider2d;
    GameManager gm;


    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        gm.AddTimeloopAffected(this);
        oPosition = new Vector3(transform.localPosition.x,transform.localPosition.y,transform.localPosition.z);
        Debug.Log($"{gameObject.name} {oPosition}");
        Vector3 oRotationVect = transform.localRotation.eulerAngles;
        oRotation = Quaternion.Euler(oRotationVect.x,oRotationVect.y,oRotationVect.z);
        oParent = transform.parent;
        sr = GetComponent<SpriteRenderer>();
        if (sr) {
            oSprite = sr.sprite;
            oColor = sr.color;
        }
        interactable = GetComponent<Interactable>();
        collider2d = GetComponent<Collider2D>();
    }

    public void Revert()
    {
        bool inBubble = false;
        if (collider2d && gameObject.activeSelf) {
            Debug.Log($"checking {gameObject.name} for bubble overlap");
            foreach (Transform bubble in gm.player.extraBubbleHolder) {
                if (collider2d && collider2d.IsTouching(bubble.GetComponent<Collider2D>())) {
                    Debug.Log("bubble touching");
                    inBubble = true;
                    break;
                }
            }
        }
        
        if (inBubble) {
            Debug.Log($"{gameObject.name} in bubble");
            return;
        }
        gameObject.SetActive(true);
        transform.parent = oParent;
        transform.localPosition = oPosition;
        transform.localRotation = oRotation;
        if (sr) {
            sr.sprite = oSprite;
            sr.color = oColor;
        }
        if (interactable) interactable.ResetState();
    }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.tag.Equals("bubble")) {
    //         Debug.Log($"{gameObject.name} enterd bubble");
    //     }
    // }

    // void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.tag.Equals("bubble")) {
    //         Debug.Log($"{gameObject.name} left bubble");
    //     }
    // }
}
