using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class SpiderBehaviour : MonoBehaviour
{
    [SerializeField] private BossMovement bossMovement;
    [SerializeField] private float rushSpeed;
    [SerializeField] private int rushDamage;
    [SerializeField] private float rushChargeTime;
    [SerializeField] private float rushDelay;
    [SerializeField] private float meleeAttackInterval, meleeAttackDelay, meleeAttackRange;
    [SerializeField] private int meleeAttackDamage;
    [SerializeField] private bool shouldSpit = true;
    [SerializeField] private float spitInterval, spitMinimumDistance;
    [SerializeField] private bool shouldShield;
    [SerializeField] private Transform webFirePos;
    [SerializeField] private Animator animator;
    [SerializeField] private GameEvent damagePlayer;
    [SerializeField] private GameObject damageLine;
    [SerializeField] private GameObject web;
    [SerializeField] private AudioSource spitSound;
    [SerializeField] private GameObject shield;
    [SerializeField] private BossHp bossHp;
    private Transform playerTransform;
    private Vector3 destination;
    private bool rush;
    private int miniSpiders;

    private void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        InvokeRepeating(nameof(MeleeAttack), meleeAttackInterval, meleeAttackInterval);
        
        if (shouldSpit)
        {
            InvokeRepeating(nameof(Spit), spitInterval, spitInterval);
        }

        if (shouldShield)
        {
            InvokeRepeating(nameof(Shield), 0, 0.5f);
        }

        miniSpiders = 3;
    }

    private void Update()
    {
        if (!rush) return;
        Rush();
    }

    private void Shield()
    {
        if (!(bossHp!.GetHpAsPercent() < 50)) return;
        shield!.SetActive(true);
        shield.transform.DetachChildren();
        CancelInvoke(nameof(Shield));
    }

    public void OnMiniSpiderDeath(Component sender, object data)
    {
        miniSpiders -= 1;
        
        if (miniSpiders <= 0)
        {
            shield!.SetActive(false);
        }
    }

    private void Spit()
    {
        if (rush) return;
        if (!(Vector3.Distance(transform.position, playerTransform.position) >= spitMinimumDistance)) return;

        ResetInvokes();
        Invoke(nameof(RushTowardsPlayerRepeatable), rushDelay);

        transform.LookAt(playerTransform);
        animator.Play("SpiderArmature|Spider_Attack");
        spitSound.Play();
        StartCoroutine(nameof(SpawnWebDelayed));
    }

    private IEnumerator SpawnWebDelayed()
    {
        yield return new WaitForSeconds(0.35f);
        Instantiate(web, webFirePos.position, Quaternion.identity);
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
        
        FacePlayer();
        DrawDamageLine();
        
        var playerPos = playerTransform.position;
        destination = new Vector3(playerPos.x, 0, playerPos.z);

        yield return new WaitForSeconds(rushChargeTime);

        animator.speed *= 3;
        damageLine.SetActive(false);
        rush = true;
    }
    
    private void FacePlayer()
    {
        var position = playerTransform.position;
        var directionToPlayer = position - transform.position;
        directionToPlayer.y = 0;
        var targetRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = targetRotation;
    }

    private void Rush()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, rushSpeed * Time.deltaTime);
        StopRushOnFinish();
    }

    private void StopRushOnFinish()
    {
        if (!HasFinishedRush()) return;
        StopRush();
    }

    private void StopRush()
    {
        rush = false;
        bossMovement.enabled = true;
        animator.speed = 1;
    }

    private bool HasFinishedRush()
    {
        return Vector3.Distance(transform.position, destination) <= 0;
    }

    private void DrawDamageLine()
    {
        var startPosition = transform.position;
        var endPosition = playerTransform.position;

        var indicatorLength = Vector3.Distance(startPosition, endPosition) / 3f;

        damageLine.transform.localPosition = new Vector3(0, 0.01f, indicatorLength / 2 + 3);
        damageLine.transform.localScale = new Vector3(3f, indicatorLength, 1f);

        damageLine.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && rush)
        {
            damagePlayer.Raise(rushDamage);
            StopRush();
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void ResetInvokes()
    {
        CancelInvoke(nameof(MeleeAttack));
        CancelInvoke(nameof(Spit));
        InvokeRepeating(nameof(MeleeAttack), meleeAttackInterval, meleeAttackInterval);
        InvokeRepeating(nameof(Spit), spitInterval, spitInterval);
    }
}