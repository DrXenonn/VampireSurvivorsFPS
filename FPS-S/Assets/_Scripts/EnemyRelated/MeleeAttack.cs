using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private EnemyScriptableObject enemyStats;
    private PlayerHealth playerHealth;
    private float timeToAttack;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MeleeAttack") && Time.time >= timeToAttack)
        {
            playerHealth.currentHealth -= enemyStats.enemyDamage;
            timeToAttack = Time.time + 1 / enemyStats.meleeAttackRate;
        }
    }
}
