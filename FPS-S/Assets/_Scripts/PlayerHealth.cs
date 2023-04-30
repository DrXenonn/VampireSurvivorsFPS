using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameEvent updateUI;
    [HideInInspector] public int currentHealth;
    [HideInInspector] public int maxHealth;
    [HideInInspector] public bool canDie = true;
    private bool isInvincible = false;

    private void Start()
    {
        maxHealth = PlayerStats.Instance.maxHealth;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (currentHealth <= 0 && canDie)
        {
            // Handle player death, probably not here in update method
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
            updateUI.Raise();
        }
    }

    public void OnHeal(Component sender, object data) 
    {
        var amount = (int)data;
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        updateUI.Raise();
    }

    public void OnMaxHealthIncrease(Component sender, object data) 
    {
        var amount = (int)data;
        maxHealth += amount;
        currentHealth += amount;
        updateUI.Raise();
    }
}
