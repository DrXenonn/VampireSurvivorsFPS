using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(NavMeshAgent))]
[DefaultExecutionOrder(1)]

public class AIUnit : MonoBehaviour
{
    private NavMeshAgent agent;
    public EnemyScriptableObject enemyManager;
    private AIManager manager;

    private void Awake()
    {
        manager = GetComponentInParent<AIManager>();
        manager.units.Add(this);
        agent = GetComponent<NavMeshAgent>();
        agent.speed = enemyManager.enemyMovementSpeed;
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