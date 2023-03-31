using UnityEngine;

public class EnemyRotation : MonoBehaviour
{
    Vector3 lookDirection;
    Transform cameraTransform;
    [SerializeField] private float rotationSpeed = 5f;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }
    void Update()
    {
        lookDirection = cameraTransform.forward;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-lookDirection), rotationSpeed * Time.deltaTime);

    }
}
