using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bubblePrefab;
    public Transform extraBubbleHolder;
    public Transform prisonHolder;
    Dictionary<string,string> gamedata = new Dictionary<string, string>();
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
        if (gm.blockInput) return;
        float rotateSpeed = (sideMovement * Time.deltaTime) / transform.position.magnitude;
        if (Input.GetKey(KeyCode.D)) {
            prisonHolder.Rotate(0f,0f,rotateSpeed);
        }
        else if (Input.GetKey(KeyCode.A)) {
            prisonHolder.Rotate(0f,0f,-rotateSpeed);
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            if (dragging) dragging.LetGo();
            else if (currentInteractables.Count > 0) GetFirstInteractable().Interact();
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            GameObject bubble = Instantiate(bubblePrefab,transform.position+Vector3.up * 0.25f,Quaternion.identity,extraBubbleHolder);
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
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log($"exit {other.gameObject.name}");
        Interactable i = other.GetComponent<Interactable>();
        if (i) currentInteractables.Remove(i);
    }

    Interactable GetFirstInteractable()
    {
        List<Interactable> validInteractables = currentInteractables.FindAll(i => i.IsValidInteractable());
        if (validInteractables.Count == 0) return null;
        foreach (Interactable i in validInteractables) if (i.priorityInteractable) return i;
        return validInteractables[0];
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
}
