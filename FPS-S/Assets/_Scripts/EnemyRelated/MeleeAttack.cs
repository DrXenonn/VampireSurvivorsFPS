using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private EnemyScriptableObject enemyStats;
    [SerializeField] private GameEvent onDealDamage;
    [SerializeField] private BerserkMode berserkMode;
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    public void DoMeleeAttack()
    {
        if (IsPlayerClose())
        {
            DealDamage();
        }
        berserkMode.BecomeNormal();
    }

    private bool IsPlayerClose()
    {
        return CalculateDistanceToPlayer() <= enemyStats.attackRange;
    }

    private float CalculateDistanceToPlayer()
    {
        return Vector3.Distance(transform.position, playerTransform.position);
    }

    private void DealDamage()
    {
        onDealDamage.Raise(enemyStats.enemyDamage);
    }
}
