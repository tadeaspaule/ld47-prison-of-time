using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform prisonHolder;
    GameManager gm;
    float sideMovement = 300f;
    
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.paused) return;
        float rotateSpeed = (sideMovement * Time.deltaTime) / transform.position.magnitude;
        if (Input.GetKey(KeyCode.D)) {
            prisonHolder.Rotate(0f,0f,rotateSpeed);
        }
        else if (Input.GetKey(KeyCode.A)) {
            prisonHolder.Rotate(0f,0f,-rotateSpeed);
        }
        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null) {
            currentInteractable.Interact();
        }
    }

    public Interactable currentInteractable;
    public Interactable carrying;
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("enter");
        Debug.Log(other);
        Interactable i = other.GetComponent<Interactable>();
        if (i) currentInteractable = i;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("exit");
        Debug.Log(other);
        Interactable i = other.GetComponent<Interactable>();
        if (i && i == currentInteractable) currentInteractable = null;
    }
}
