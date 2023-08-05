using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField] private float speed, stoppingDistance, rotationSpeed;
    public float height;
    private Transform playerPos;

    private void Start()
    {
        playerPos = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        MoveTowardsPlayer();
        LookAtPlayer();
    }

    private bool IsCloseToPlayer()
    {
        return stoppingDistance > Vector3.Distance(transform.position, playerPos.position);
    }

    private void MoveTowardsPlayer()
    {
        if (IsCloseToPlayer()) return;
        var position = playerPos.position;
        var ignoreYPos = new Vector3(position.x, height, position.z);
        transform.position = Vector3.MoveTowards(transform.position, ignoreYPos, speed * Time.deltaTime);
    }
    

    private void LookAtPlayer()
    {
        var directionToPlayer = playerPos.position - transform.position;
        directionToPlayer.y = 0f;

        if (directionToPlayer == Vector3.zero) return;

        var targetRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}