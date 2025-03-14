using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public float smoothSpeed;
    public Transform objectToFollow;
    public Vector3 cameraOffset;
    private void Awake()
    {
        smoothSpeed = 1 - smoothSpeed;
    }
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, objectToFollow.position, smoothSpeed);
        transform.position += cameraOffset;
    }
}
