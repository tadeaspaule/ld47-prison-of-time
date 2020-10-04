using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFog : MonoBehaviour
{
    GameManager gm;
    System.Random r = new System.Random();
    SpriteRenderer sr;
    Color c;
    int[] c1;
    int[] c2;
    Vector3 dest;
    float timer = 0f;
    int phase = 1; // 1 -> intensify, 2 -> dim, 3-> sharp fast intensify at loop time, 4 --> sharp fast dim at loop time
    int counter = 0;
    float normalSpd = 0.03f;
    float sharpSpd = 0.15f;
    
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        sr = GetComponent<SpriteRenderer>();
        c = sr.color;
        // c1 = new int[]{r.Next(255),r.Next(255),r.Next(255)};
        // c2 = new int[]{r.Next(255),r.Next(255),r.Next(255)};
        // sr.color = new Color(c1[0]/255f,c1[1]/255f,c1[2]/255f);
        // dest = new Vector3(Random.Range(-4f,4f),Random.Range(-4f,4f),0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.paused) return;
        timer += Time.deltaTime;
        if (phase == 1) sr.color = new Color(c.r,c.g,c.b,sr.color.a + Time.deltaTime * normalSpd);
        else if (phase == 2) sr.color = new Color(c.r,c.g,c.b,sr.color.a - Time.deltaTime * normalSpd);
        else if (phase == 3) sr.color = new Color(c.r,c.g,c.b,sr.color.a + Time.deltaTime * sharpSpd);
        else if (phase == 4) sr.color = new Color(c.r,c.g,c.b,sr.color.a - Time.deltaTime * sharpSpd);
        if (timer >= 1f) {
            timer = 0f;
            // counter++;
            // if (counter == TimeloopAffected.maxTimer) phase = 3;
            // else if (counter == TimeloopAffected.maxTimer + 1) phase = 4;
            // else if (counter == TimeloopAffected.maxTimer + 2) {
            //     phase = 1;
            //     counter = 0;
            //     Debug.Log($"Alpha after big dim {sr.color.a}");
            // }
            // else phase = 1 + (phase % 2);
            phase = 1 + (phase % 2);
        } 
        // for (int i = 0; i < 2; i++) {
        //     if (c1[i] < c2[i]) c1[i]++;
        //     else if (c1[i] > c2[i]) c1[i]++;
        //     else c2[i] = r.Next(255);
        // }
        // sr.color = new Color(c1[0]/255f,c1[1]/255f,c1[2]/255f);
        // if (Vector3.Distance(dest,transform.position) < 1f) dest = new Vector3(Random.Range(-4f,4f),Random.Range(-4f,4f),0f);
        // Vector3 moveVect = (dest - transform.position).normalized;
        // transform.position += moveVect * Time.deltaTime * 0.001f;
    }
}
