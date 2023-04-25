using UnityEngine;

public class SacrificialStrength_U : MonoBehaviour
{
    private PlayerHealth playerHealth;
    [SerializeField] private int bonusMaxHealth = 10;
    [SerializeField] private float hpThreshold = 20;
    [SerializeField] private float bonusDamagePercent = 25;
    [SerializeField] private float bonusMovementSpeedPercent = 25;
    private bool buffsApplied = false;
    private float bonusMovementSpeed;
    private int bonusDamage;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerHealth.maxHealth += bonusMaxHealth;
        playerHealth.currentHealth += bonusMaxHealth;
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
        PlayerStats.Instance.movementSpeed -= bonusMovementSpeed;
        PlayerStats.Instance.primaryDamage -= bonusDamage;
    }

    private void SetBuffs()
    {
        bonusMovementSpeed = PlayerStats.Instance.GainBonus("movementSpeed", bonusMovementSpeedPercent);
        bonusDamage = (int)PlayerStats.Instance.GainBonus("primaryDamage", bonusDamagePercent);
    }
}
