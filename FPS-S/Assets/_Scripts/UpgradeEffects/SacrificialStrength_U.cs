using UnityEngine;

public class SacrificialStrength_U : MonoBehaviour
{
    private PlayerHealth playerHealth;
    [SerializeField] private float bonusMaxHealth = 10;
    [SerializeField] private float hpThreshold = 20;
    [SerializeField] private float bonusDamagePercent = 25;
    [SerializeField] private float bonusMovementSpeedPercent = 25;
    private int originalPrimaryDamage;
    private float originalMovementSpeed;
    private bool buffsApplied = false;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerHealth.maxHealth += bonusMaxHealth;
        playerHealth.currentHealth += bonusMaxHealth;
        originalPrimaryDamage = PlayerStats.Instance.primaryDamage;
        originalMovementSpeed = PlayerStats.Instance.movementSpeed;
    }

    private void Update()
    {
        if (playerHealth.currentHealth <= hpThreshold && !buffsApplied)
        {
            SetBuffs();
            buffsApplied = true;
        }
        else if (playerHealth.currentHealth > hpThreshold && buffsApplied)
        {
            ResetBuffs();
            buffsApplied = false;
        }
    }

    private void ResetBuffs()
    {
        PlayerStats.Instance.primaryDamage = originalPrimaryDamage;
        PlayerStats.Instance.movementSpeed = originalMovementSpeed;
    }

    private void SetBuffs()
    {
        PlayerStats.Instance.GainBonus("movementSpeed", bonusMovementSpeedPercent);
        PlayerStats.Instance.GainBonus("primaryDamage", bonusDamagePercent);
    }
}
