using UnityEngine;

public class LastStand_U : MonoBehaviour
{
    private PlayerHealth playerHealth;
    [SerializeField] private float invincibilityTime = 5f;
    [SerializeField] private int lastChanceHealth = 20;
    [SerializeField] private int bonusDamagePercent = 5;
    [SerializeField] private float bonusFireRatePercent = 15f;
    [SerializeField] private float bonusMovementSpeedPercent = 5f;
    [SerializeField] private GameEvent onHeal;
    [SerializeField] GameObject aoeEffect;


    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerHealth.canDie = false;
    }

    private void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            playerHealth.BecomeInvincible(invincibilityTime);
            onHeal.Raise(lastChanceHealth);
            Instantiate(aoeEffect, playerHealth.gameObject.transform);
            Destroy(gameObject, invincibilityTime + 0.1f);
            GainBonuses();
        }
    }

    private void GainBonuses()
    {
        PlayerStats.Instance.GainBonus("primaryDamage", bonusDamagePercent);
        PlayerStats.Instance.GainBonus("movementSpead", bonusMovementSpeedPercent);
        PlayerStats.Instance.GainBonus("fireRate", bonusFireRatePercent);
    }

    private void OnDestroy()
    {
        playerHealth.canDie = true;
    }
}
