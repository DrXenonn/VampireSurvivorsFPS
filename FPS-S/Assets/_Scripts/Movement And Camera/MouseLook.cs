using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float mouseSens = 100f;
    [SerializeField] private Transform playerBody;
    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float MouseX = Input.GetAxis("Mouse X") * mouseSens;
        float MouseY = Input.GetAxis("Mouse Y") * mouseSens;

        playerBody.Rotate(Vector3.up, MouseX);

        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
