using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Bubble : MonoBehaviour
{
    SpriteRenderer sr;
    Light2D light2d;
    SpriteMask spriteMask;
    float amount = 0f;
    float spd = 0.45f;
    float diff = 0.15f;
    float scaleMult = 0.5f;
    float alphaMult = 0.7f;
    Color c;
    GameManager gm;
    float spdMin = 0.3f;
    float spdMax = 0.6f;

    bool hidden = false;
    
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        spriteMask = GetComponent<SpriteMask>();
        c = sr.color;
        gm = FindObjectOfType<GameManager>();
        light2d = transform.GetChild(0).GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.paused) return;
        amount += Time.deltaTime * spd;
        float mult = (0.6f+amount*scaleMult) / 0.6f;
        transform.localScale = new Vector3(0.6f*mult,0.6f*mult,1f);
        light2d.pointLightOuterRadius = (0.3f * mult) * (hidden ? 0 : 1);
        sr.color = new Color(c.r,c.g,c.b,(c.a+amount*alphaMult) * (hidden ? 0 : 1));
        if (amount >= diff) {
            spd = -Random.Range(spdMin,spdMax);
            amount = diff;
            diff = 0.15f + Random.Range(-0.1f,0.1f);
        }
        else if (amount <= 0f) {
            spd = Random.Range(spdMax,spdMax);
            amount = 0f;
            diff = 0.15f + Random.Range(-0.1f,0.1f);
        }
    }

    public void ToggleVisuals(bool active)
    {
        hidden = true;
        spriteMask.enabled = active;
        sr.color = new Color(sr.color.r,sr.color.g,sr.color.b,0f);
    }
}
