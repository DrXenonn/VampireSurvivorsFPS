using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(NavMeshAgent))]
[DefaultExecutionOrder(1)]

public class AIUnit : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    public EnemyScriptableObject enemyManager;
    private AIManager manager;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        manager = GetComponentInParent<AIManager>();
        manager.units.Add(this);
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = enemyManager.enemyMovementSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ChangeAnimation"))
        {
            animator.SetBool("isCloseToPlayer", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ChangeAnimation"))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position), 5f * Time.deltaTime);
            animator.SetBool("isCloseToPlayer", false);
        }
    }

    public void MoveTo(Vector3 position)
    {
        agent.SetDestination(position);
    }

    private void OnDestroy()
    {
        manager.units.Remove(this);
        Destroy(this);
    }
}