using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFog : MonoBehaviour
{
    GameManager gm;
    System.Random r = new System.Random();
    SpriteRenderer sr;
    int[] c1;
    int[] c2;
    Vector3 dest;
    
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        sr = GetComponent<SpriteRenderer>();
        c1 = new int[]{r.Next(255),r.Next(255),r.Next(255)};
        c2 = new int[]{r.Next(255),r.Next(255),r.Next(255)};
        sr.color = new Color(c1[0]/255f,c1[1]/255f,c1[2]/255f);
        dest = new Vector3(Random.Range(-4f,4f),Random.Range(-4f,4f),0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.paused) return;
        for (int i = 0; i < 2; i++) {
            if (c1[i] < c2[i]) c1[i]++;
            else if (c1[i] > c2[i]) c1[i]++;
            else c2[i] = r.Next(255);
        }
        sr.color = new Color(c1[0]/255f,c1[1]/255f,c1[2]/255f);
        if (Vector3.Distance(dest,transform.position) < 1f) dest = new Vector3(Random.Range(-4f,4f),Random.Range(-4f,4f),0f);
        Vector3 moveVect = (dest - transform.position).normalized;
        transform.position += moveVect * Time.deltaTime * 0.001f;
    }
}
