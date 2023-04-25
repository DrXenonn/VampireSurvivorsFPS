using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private GameEvent updateHealthBar;
    [HideInInspector] public int currentHealth;
    [HideInInspector] public int maxHealth;
    [HideInInspector] public bool canDie = true;
    private bool isInvincible = false;

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
            currentHealth += 1;
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

    public void TakeDamage(Component sender, object data)
    {
        if (!isInvincible)
        {
            var damage = (int)data;
            currentHealth -= damage;
            updateHealthBar.Raise();
        }
    }

    public void OnHeal(Component sender, object data) 
    {
        var amount = (int)data;
        currentHealth += amount;
        updateHealthBar.Raise();
    }

    public void OnMaxHealthIncrease(Component sender, object data) 
    {
        var amount = (int)data;
        maxHealth += amount;
        currentHealth += amount;
        updateHealthBar.Raise();
    }
}
