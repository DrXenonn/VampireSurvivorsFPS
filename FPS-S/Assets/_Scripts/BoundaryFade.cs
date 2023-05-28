using UnityEngine;

public class BoundaryFade : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform boundaryTransform;
    [SerializeField] private float rotationSpeed;


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.Play("BoundaryFade");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.Play("BoundaryFadeOut");
        }
    }

    private void Update()
    {
        boundaryTransform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
