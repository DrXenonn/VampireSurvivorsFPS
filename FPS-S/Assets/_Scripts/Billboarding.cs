using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboarding : MonoBehaviour
{
    Vector3 lookDirection;
    Transform cameraTransform;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }
    void Update()
    {
        lookDirection = cameraTransform.forward;
        transform.forward = lookDirection;
    }
}
