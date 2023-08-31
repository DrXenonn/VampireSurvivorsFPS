using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float mouseSens;
    [SerializeField] private Transform playerBody;
    private float xRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        mouseSens = PlayerPrefs.GetFloat("MouseSensitivity", 1);
    }

    private void Update()
    {
        var mouseX = Input.GetAxis("Mouse X") * mouseSens;
        var mouseY = Input.GetAxis("Mouse Y") * mouseSens;

        playerBody.Rotate(Vector3.up, mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
