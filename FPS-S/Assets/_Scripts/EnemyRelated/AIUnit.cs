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
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        agent.speed = enemyManager.enemyMovementSpeed;
        manager.units.Add(this);
        if (!agent.isOnNavMesh)
        {
            Destroy(gameObject);
            manager.units.Remove(this);
        }
    }

    private void OnDisable()
    {
        manager.units.Remove(this);
    }

    public void MoveTo(Vector3 position)
    {
        agent.SetDestination(position);
    }
}
