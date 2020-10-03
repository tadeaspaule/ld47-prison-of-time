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

    public float timer = 0f;
    public static float maxTimer = 50f;
    bool inBubble = false;



    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        oPosition = new Vector3(transform.localPosition.x,transform.localPosition.y,transform.localPosition.z);
        // Debug.Log($"{gameObject.name} {oPosition}");
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

    void Update()
    {
        if (gm.paused) return;
        if (!inBubble) timer += Time.deltaTime;
        if (timer >= maxTimer) {
            if (!gameObject.name.Equals("player")) Revert();
            else {
                gm.RevertPlayer();
            }
        }
    }

    public void Revert()
    {        
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("bubble")) {
            // Debug.Log($"{gameObject.name} enterd bubble");
            inBubble = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("bubble")) {
            // Debug.Log($"{gameObject.name} left bubble");
            inBubble = false;
        }
    }
}
