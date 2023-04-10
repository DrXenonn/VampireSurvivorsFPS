using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [HideInInspector] public float currentHealth;
    [HideInInspector] public float maxHealth;
    [HideInInspector] public bool canDie = true;
    [HideInInspector] public bool isInvincible = false;


    private void Start()
    {
        currentHealth = playerStats.maxHealth;
        maxHealth = playerStats.maxHealth;
    }

    private void Update()
    {
        if (currentHealth <= 0 && canDie)
        {
            // Handle player death
        }
    }

    public void RegenerateHealth(float regenThreshold)
    {
        if (GetPlayerHealthInPercent() < regenThreshold)
        {
            currentHealth += 1f;
            currentHealth = Mathf.Min(currentHealth, maxHealth);
        }
    }

    public float GetPlayerHealthInPercent()
    {
        return currentHealth / maxHealth * 100;
    }

    public void BecomeInvincible(float duration)
    {
        if (!isInvincible)
        {
            isInvincible = true;
            StartCoroutine(InvincibilityCoroutine(duration));
        }
    }

    private IEnumerator InvincibilityCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        isInvincible = false;
    }
}
