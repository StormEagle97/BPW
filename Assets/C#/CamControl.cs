using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour

{
    public Transform currentMount;
    public Camera cam;
    public float speedFactor = 0.04f;
    public float zoomFactor = 1.0f;
    public Vector3 lastPosition; 

    void Start ()
    {
        lastPosition = transform.position;
    } 

    void Update ()
    {
        cam.transform.position = Vector3.Lerp (transform.position, currentMount.position, speedFactor);
        cam.transform.rotation = Quaternion.Slerp (transform.rotation, currentMount.rotation, speedFactor);

        float velocity = Vector3.Magnitude (transform.position - lastPosition);
        cam.fieldOfView = 60 + velocity * zoomFactor;

        lastPosition = transform.position;
    }

    public void setMount(Transform newMount)
    {
        currentMount = newMount;
    }
}﻿