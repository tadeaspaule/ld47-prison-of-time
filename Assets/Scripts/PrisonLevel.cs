using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonLevel : MonoBehaviour
{
    int level;
    public GameObject lockdownAlarmPrefab;
    
    // Start is called before the first frame update
    void Start()
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
        }
    }
}
