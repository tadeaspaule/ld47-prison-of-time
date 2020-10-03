using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LockdownAlarm : MonoBehaviour
{
    public Light2D light2D;
    float startValue;
    bool growing = true;
    float diff = 0.6f;
    float changeMult = 3f;
    
    // Start is called before the first frame update
    void Start()
    {
        startValue = light2D.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (growing) {
            light2D.intensity += Time.deltaTime * changeMult;
            if (light2D.intensity >= startValue + diff) growing = false;
        }
        else {
            light2D.intensity -= Time.deltaTime * changeMult;
            if (light2D.intensity <= startValue - diff) growing = true;
        }
    }
}
