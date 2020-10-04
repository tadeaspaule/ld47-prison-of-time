using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    Vector3 camOffset = new Vector3(0f,0.2f,-10f);

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + camOffset;
    }
}
