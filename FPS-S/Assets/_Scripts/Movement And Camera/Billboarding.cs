using UnityEngine;

public class Billboarding : MonoBehaviour
{
    private Camera mainCamera;
    [HideInInspector]public float distanceToCamera;
    
    private void Awake()
    {
        mainCamera = Camera.main;
        distanceToCamera = Vector3.Distance(mainCamera.transform.position, transform.position);
    }

    private void Update()
    {
        transform.forward = mainCamera.transform.forward;
    }
}