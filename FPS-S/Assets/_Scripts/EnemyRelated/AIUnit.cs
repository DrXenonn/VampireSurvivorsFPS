using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(NavMeshAgent))]
[DefaultExecutionOrder(1)]

public class AIUnit : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent agent;
    public EnemyScriptableObject enemyManager;
    private AIManager manager;

    private void Awake()
    {
        manager = GetComponentInParent<AIManager>();
        manager.units.Add(this);
        agent = GetComponent<NavMeshAgent>();
        agent.speed = enemyManager.enemyMovementSpeed;
    }

    private void OnEnable()
    {
        if (!agent.isOnNavMesh)
        {
            Destroy(gameObject);
            manager.units.Remove(this);
        }
    }

    public void MoveTo(Vector3 position)
    {
        agent.SetDestination(position);
    }
}
