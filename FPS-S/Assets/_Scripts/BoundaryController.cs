using UnityEngine;

public class BoundaryController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private float knockbackForce = 10f;
    private bool isPlayerOutside = false;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOutside = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOutside = false;
        }
    }

    private void Update()
    {
        if (isPlayerOutside)
        {
            PushPlayerInside();
        }
    }

    private void PushPlayerInside()
    {
        playerRigidbody.AddForce(FindDirectionToPush() * knockbackForce, ForceMode.Impulse);
    }

    private Vector3 FindDirectionToPush()
    {
        Vector3 direction = transform.position - playerTransform.position;
        return direction.normalized;
    }
}
