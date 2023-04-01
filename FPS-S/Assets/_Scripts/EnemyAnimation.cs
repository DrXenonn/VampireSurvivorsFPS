using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator animator;
    private Transform playerTransform;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ChangeAnimation"))
        {
            animator.SetBool("isCloseToPlayer", true);
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
}
