using System.Collections;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class SpiderBehaviour : MonoBehaviour
{
    [SerializeField] private BossMovement bossMovement;
    [SerializeField] private float rushSpeed;
    [SerializeField] private int rushDamage;
    [SerializeField] private float rushChargeTime;
    [SerializeField] private float meleeAttackInterval, meleeAttackDelay, meleeAttackRange;
    [SerializeField] private int meleeAttackDamage;
    [SerializeField] private float spitInterval, spitMinimumDistance;
    [SerializeField] private Transform webFirePos;
    [SerializeField] private Animator animator;
    [SerializeField] private GameEvent damagePlayer;
    [SerializeField] private GameObject damageLine;
    [SerializeField] private GameObject web;
    private Transform playerTransform;
    private bool rush;
    private Vector3 destination;

    private void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        InvokeRepeating(nameof(MeleeAttack), meleeAttackInterval, meleeAttackInterval);
        InvokeRepeating(nameof(Spit), spitInterval, spitInterval);
    }

    private void Update()
    {
        if (!rush) return;
        Rush();
    }

    private void Spit()
    {
        if (rush) return;
        if (!(Vector3.Distance(transform.position, playerTransform.position) >= spitMinimumDistance)) return;
        
        ResetInvokes();
        Invoke(nameof(RushTowardsPlayerRepeatable),1.75f);

        transform.LookAt(playerTransform);
        animator.Play("SpiderArmature|Spider_Attack");
        StartCoroutine(nameof(SpawnWebDelayed));
    }

    private IEnumerator SpawnWebDelayed()
    {
        yield return new WaitForSeconds(0.35f);
        Instantiate(web,webFirePos.position, Quaternion.identity);
    }

    private void MeleeAttack()
    {
        if (!IsCloseToPlayer() || rush) return;
        StartCoroutine(nameof(MeleeAttackCoroutine));
    }

    private IEnumerator MeleeAttackCoroutine()
    {
        transform.LookAt(playerTransform);
        animator.Play("SpiderArmature|Spider_Attack");
        yield return new WaitForSeconds(meleeAttackDelay);
        if (IsCloseToPlayer())
        {
            damagePlayer.Raise(meleeAttackDamage);
        }
    }

    private bool IsCloseToPlayer()
    {
        return Vector3.Distance(transform.position, playerTransform.position) < meleeAttackRange;
    }

    private void RushTowardsPlayerRepeatable()
    {
        ResetInvokes();
        StartCoroutine(nameof(RushTowardsPlayer));
    }

    private IEnumerator RushTowardsPlayer()
    {
        bossMovement.enabled = false;
        transform.LookAt(playerTransform);

        DrawDamageLine();
        var playerPos = playerTransform.position;
        destination = new Vector3(playerPos.x, 0, playerPos.z);

        yield return new WaitForSeconds(rushChargeTime);

        animator.speed *= 3;
        damageLine.SetActive(false);
        rush = true;
    }

    private void Rush()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, rushSpeed * Time.deltaTime);
        StopRush();
    }

    private void StopRush()
    {
        if (!(GetDistanceToDestination() <= 0)) return;
        rush = false;
        bossMovement.enabled = true;
        animator.speed = 1;
    }

    private float GetDistanceToDestination()
    {
        return Vector3.Distance(transform.position, destination);
    }

    private void DrawDamageLine()
    {
        var startPosition = transform.position;
        var endPosition = playerTransform.position;

        var indicatorHeight = Vector3.Distance(startPosition, endPosition) / 3f;

        damageLine.transform.localPosition = new Vector3(0, float.Epsilon, indicatorHeight / 2 + 3);
        damageLine.transform.localScale = new Vector3(3f, indicatorHeight, 1f);

        damageLine.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && rush)
        {
            damagePlayer.Raise(rushDamage);
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void ResetInvokes()
    {
        CancelInvoke();
        InvokeRepeating(nameof(MeleeAttack), meleeAttackInterval, meleeAttackInterval);
        InvokeRepeating(nameof(Spit), spitInterval, spitInterval);
    }
}