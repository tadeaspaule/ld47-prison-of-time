﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{    
    SpriteRenderer sr;
    
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    const string watchTag = "guardStatue";
    public bool scannerHappy = false;
    int statuesInScanner = 0;
    Color sadColor = new Color(1f,0.3f,0.3f,0.25f);
    Color happyColor = new Color(0.3f,1f,0.3f,0.25f);
    

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"{other.gameObject.name} entered scanner with tag {other.tag}");
        if (other.tag.Equals(watchTag)) {
            statuesInScanner++;
            UpdateHappyState();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log($"{other.gameObject.name} left scanner with tag {other.tag}");
        if (other.tag.Equals(watchTag)) {
            statuesInScanner--;
            UpdateHappyState();
        }
    }

    void UpdateHappyState()
    {
        if (statuesInScanner > 0) {
            sr.color = happyColor;
            scannerHappy = true;
        }
        else {
            sr.color = sadColor;
            scannerHappy = false;
        }
    }
}
