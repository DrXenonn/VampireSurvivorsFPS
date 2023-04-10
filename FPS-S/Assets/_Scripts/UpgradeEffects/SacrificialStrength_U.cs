using UnityEngine;

public class SacrificialStrength_U : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private PlayerMovement playerMovement;
    [SerializeField] private float bonusMaxHealth = 10;
    [SerializeField] private float hpThresholdPercent = 20;
    [SerializeField] private float bonusDamagePercent = 25;
    [SerializeField] private float bonusMovementSpeedPercent = 25;
    private int originalPrimaryDamage;
    private float originalMovementSpeed;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerHealth.maxHealth += bonusMaxHealth;
        playerHealth.currentHealth += bonusMaxHealth;
        originalPrimaryDamage = PlayerStats.Instance.primaryDamage;
        originalMovementSpeed = playerMovement.currentMovementSpeed;
    }
    private void Update()
    {
        if (playerHealth.GetPlayerHealthInPercent() <= hpThresholdPercent)
        {
            SetBuffs();
        }
        else
        {
            ResetBuffs();
        }
    }

    private void ResetBuffs()
    {
        PlayerStats.Instance.primaryDamage = originalPrimaryDamage;
        playerMovement.currentMovementSpeed = originalMovementSpeed;
    }
    private void SetBuffs()
    {
        PlayerStats.Instance.primaryDamage = GetCalculatedDamage();
        playerMovement.currentMovementSpeed = GetCalculatedMovementSpeed();
    }
    private int GetCalculatedDamage()
    {
        int calculatedDamage = 1;
        calculatedDamage = originalPrimaryDamage * Mathf.RoundToInt(100 + bonusDamagePercent) / 100;
        return calculatedDamage;
    }
    private float GetCalculatedMovementSpeed()
    {
        float calculatedMovementSpeed = 0;
        calculatedMovementSpeed = originalMovementSpeed * (100 + bonusMovementSpeedPercent) / 100;
        return calculatedMovementSpeed;
    }
}
