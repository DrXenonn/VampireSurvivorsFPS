using UnityEngine;

public class Billboarding : MonoBehaviour
{
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        transform.forward = mainCamera.transform.forward;
    }
}