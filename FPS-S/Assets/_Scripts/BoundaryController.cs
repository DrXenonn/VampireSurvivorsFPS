using UnityEngine;

public class BoundaryController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private float knockbackForce = 10f;
    private bool isPlayerOutside;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOutside = true;
            InvokeRepeating(nameof(PushPlayerInside), 0, 0.05f);
        }
    }
    

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOutside = false;
        }
    }

    private void PushPlayerInside()
    {
        if (isPlayerOutside)
        {
            playerRigidbody.AddForce(FindDirectionToPush() * knockbackForce, ForceMode.Impulse);
        }
    }

    private Vector3 FindDirectionToPush()
    {
        Vector3 direction = transform.position - playerTransform.position;
        return direction.normalized;
    }
}