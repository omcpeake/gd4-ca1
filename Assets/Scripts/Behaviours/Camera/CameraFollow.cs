using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://www.youtube.com/watch?v=d_0dAwk3wqI&ab_channel=Vinny
public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    private Vector3 cameraOffset;

    [Range(0.01f, 1.0f)]
    public float smoothness = 0.5f;
    void Start()
    {
        cameraOffset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = player.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPos, smoothness);
    }
}
