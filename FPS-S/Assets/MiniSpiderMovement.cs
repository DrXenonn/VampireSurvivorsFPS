using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MiniSpiderMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Animator animator;
    [SerializeField] private GameEvent miniSpiderDeath;
    private Transform playerTransform;

    [SerializeField] private float descentSpeed;
    [SerializeField] private float targetY;

    private void OnEnable()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        animator.speed = 2;
        StartCoroutine(nameof(GoDown));
    }

    private IEnumerator GoDown()
    {
        var position = transform.position;

        while (position.y > targetY + 0.01f)
        {
            yield return new WaitForSeconds(0.01f);

            position.y = Mathf.Max(position.y - descentSpeed, targetY);
            transform.position = position;
        }

        position.y = targetY;
        transform.position = position;

        navMeshAgent.enabled = true;
    }
    
    private void Update()
    {
        if (navMeshAgent.enabled)
        {
            navMeshAgent.SetDestination(playerTransform.position);
        }
    }

    private void OnDestroy()
    {
        miniSpiderDeath.Raise();
    }
}
