using System;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private Transform playerTransform;
    [SerializeField] private float damping = 1f;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnEnable()
    {
        animator.speed = 1;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ChangeAnimation"))
        {
            animator.SetBool("isCloseToPlayer", true);
        }

        if (other.CompareTag("LookDirectlyAtPlayer"))
        {
            transform.LookAt(playerTransform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ChangeAnimation"))
        {
            animator.SetBool("isCloseToPlayer", false);
        }
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
        Quaternion rotation = Quaternion.LookRotation(targetPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }
}
