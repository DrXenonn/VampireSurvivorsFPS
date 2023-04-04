
using UnityEngine;

public class CheckForCollision : MonoBehaviour
{
    [SerializeField] private string tagToCheckTrigger;
    [HideInInspector] public bool hasEntered = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(tagToCheckTrigger))
        {
            hasEntered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tagToCheckTrigger))
        {
            hasEntered = false;
        }
    }
}
