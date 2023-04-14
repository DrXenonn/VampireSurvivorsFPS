using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(NavMeshAgent))]
[DefaultExecutionOrder(1)]

public class AIUnit : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent agent;
    public EnemyScriptableObject enemyManager;
    private AIManager manager;
    private bool isPathfindingFailed = false;

    private void Awake()
    {
        manager = GetComponentInParent<AIManager>();
        manager.units.Add(this);
        agent = GetComponent<NavMeshAgent>();
        agent.speed = enemyManager.enemyMovementSpeed;
    }

    public void MoveTo(Vector3 position)
    {
        NavMeshPath path = new NavMeshPath();
        if (NavMesh.CalculatePath(transform.position, position, NavMesh.AllAreas, path))
        {
            agent.SetPath(path);
            isPathfindingFailed = false;
        }
        else
        {
            isPathfindingFailed = true;
        }
    }

    private void Update()
    {
        if (isPathfindingFailed && agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            // If the pathfinding failed previously and the agent has stopped moving, try again
            MoveTo(agent.destination);
        }
    }

    private void OnDestroy()
    {
        manager.units.Remove(this);
        // Delay destroying the agent to give it a chance to try and find a path again
        Destroy(gameObject, 1f);
    }
}
