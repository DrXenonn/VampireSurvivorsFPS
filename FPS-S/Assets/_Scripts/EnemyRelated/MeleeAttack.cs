using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private EnemyScriptableObject enemyStats;
    [SerializeField] private CheckForCollision enemyHand;
    [SerializeField] private GameEvent onDealDamage;

    public void DoMeleeAttack()
    {
        if (enemyHand.hasEntered)
        {
            onDealDamage.Raise(enemyStats.enemyDamage);
        }
    }
}
