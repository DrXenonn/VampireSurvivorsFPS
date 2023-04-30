using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimation : MonoBehaviour
{
    private Animator animator;
    private Transform playerTransform;
    private NavMeshAgent agent;
    [SerializeField] private float speedWhenCloseToPlayer;
    [SerializeField] private float damping = 1f;
    private float startingSpeed;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        startingSpeed = agent.speed;
    }

    private void OnEnable()
    {
        if (agent != null)
        {
            agent.speed = startingSpeed;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ChangeAnimation"))
        {
            animator.SetBool("isCloseToPlayer", true);
            agent.speed = speedWhenCloseToPlayer;
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
            agent.speed = startingSpeed;
        }
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
        Quaternion rotation = Quaternion.LookRotation(targetPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }
}
