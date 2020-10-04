using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public Transform prisonHolder;
    public TextMeshProUGUI interactHintText;
    Dictionary<string,string> gamedata = new Dictionary<string, string>();
    GameManager gm;
    Transform objectHolder;
    public Collider2D playerCollider;
    float sideMovement = 120f;
    bool moving = false;
    float movingTilt = 0f;
    float movingMaxTilt = 7f;
    float movingTiltSpd = 100f;
    public List<string> bubbleTools = new List<string>();
    public List<string> placedBubbletools = new List<string>();
    
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        objectHolder = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.paused) return;
        if (gm.blockInput) return;
        float rotateSpeed = (sideMovement * Time.deltaTime) / transform.position.magnitude;
        if (Input.GetKey(KeyCode.D)) {
            prisonHolder.Rotate(0f,0f,rotateSpeed);
            moving = true;
            objectHolder.rotation = Quaternion.identity;
        }
        else if (Input.GetKey(KeyCode.A)) {
            prisonHolder.Rotate(0f,0f,-rotateSpeed);
            objectHolder.rotation = Quaternion.Euler(0f,180f,0f);
            moving = true;
        }
        else moving = false;
        if (moving) {
            movingTilt += Time.deltaTime * movingTiltSpd;
            transform.rotation = Quaternion.Euler(0f,0f,movingTilt);
            if (movingTilt >= movingMaxTilt) movingTilt = movingMaxTilt;
            else if (movingTilt <= -movingMaxTilt) movingTilt = -movingMaxTilt;
            if (movingTilt >= movingMaxTilt || movingTilt <= -movingMaxTilt) movingTiltSpd *= -1;
        }
        else transform.rotation = Quaternion.identity;
        if (Input.GetKeyDown(KeyCode.E)) {
            // List<Interactable> validInteractables = GetValidInteractables();
            // Debug.Log(validInteractables);
            if (dragging) dragging.LetGo();
            else {
                Interactable i = GetFirstInteractable();
                Debug.Log(i);
                if (i) i.Interact();
            }
            UpdateHintText();
        }
        if (Input.GetKeyDown(KeyCode.Space) && bubbleTools.Count > 0) {
            gm.PlaceBubble(transform.position+Vector3.up * 0.25f);
        }
    }

    public List<Interactable> currentInteractables = new List<Interactable>();
    public Carriable carrying;
    public Draggable dragging;
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"enter {other.gameObject.name}");
        Interactable i = other.GetComponent<Interactable>();
        if (i) currentInteractables.Add(i);
        UpdateHintText();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log($"exit {other.gameObject.name}");
        Interactable i = other.GetComponent<Interactable>();
        if (i) currentInteractables.Remove(i);
        UpdateHintText();
    }

    Interactable GetFirstInteractable()
    {
        List<Interactable> validInteractables = GetValidInteractables();
        if (validInteractables.Count == 0) return null;
        foreach (Interactable i in validInteractables) if (i.priorityInteractable) return i;
        return validInteractables[0];
    }

    List<Interactable> GetValidInteractables()
    {
        return currentInteractables.FindAll(i => i.IsValidInteractable() && i.GetComponent<Collider2D>().IsTouching(playerCollider));
    }

    public void UpdateGameData(string key, string value)
    {
        if (gamedata.ContainsKey(key)) gamedata[key] = value;
        else gamedata.Add(key,value);
    }

    public string GetGameData(string key)
    {
        if (gamedata.ContainsKey(key)) return gamedata[key];
        else return null;
    }

    public void WipeGameData()
    {
        gamedata.Clear();
    }

    public void UpdateHintText()
    {
        Interactable i = GetFirstInteractable();
        if (i) interactHintText.text = i.hintName;
        else interactHintText.text = "";
    }
}
