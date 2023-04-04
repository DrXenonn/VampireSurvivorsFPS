using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private EnemyScriptableObject enemyStats;
    [SerializeField] private CheckForCollision enemyHand;
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    public void DoMeleeAttack()
    {
        if (enemyHand.hasEntered)
        {
            playerHealth.currentHealth -= enemyStats.enemyDamage;
        }
    }
}
