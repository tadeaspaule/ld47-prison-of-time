using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PrisonLevel : MonoBehaviour
{
    int level;
    public GameObject lockdownAlarmPrefab;
    List<Light2D> lights = new List<Light2D>();
    
    // Start is called before the first frame update
    void Awake()
    {
        level = int.Parse(gameObject.name.Substring(5));
        // generate lockdown alarms
        float r = 2.88f + level * 0.625f;
        int n = 10;
        float radPart = (Mathf.PI * 2f) / n;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        for (int i = 0; i < n; i++) {
            float x = r*Mathf.Cos(radPart*i);
            float y = r*Mathf.Sin(radPart*i);
            GameObject go = Instantiate(lockdownAlarmPrefab,Vector3.zero,Quaternion.identity,transform);
            go.transform.localPosition = new Vector3(x,y,0f);
            go.transform.localRotation = Quaternion.Euler(0f,0f,180f * (radPart*i / Mathf.PI) - 90f);
            go.GetComponent<SpriteRenderer>().sortingLayerID = sr.sortingLayerID;
            lights.Add(go.GetComponent<Light2D>());
        }
    }

    public void ToggleLockdown(bool value)
    {
        foreach (Light2D lightObj in lights) lightObj.enabled = value;
    }
}
