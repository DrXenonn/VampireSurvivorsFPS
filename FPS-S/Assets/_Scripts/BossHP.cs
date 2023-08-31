using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossHp : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private GameEvent deathEvent;
    [SerializeField] private Transform dmgPopupPos;
    [SerializeField] private Animator animator;
    [SerializeField] private Image healthBar;
    [SerializeField] private GameEvent win;
    [SerializeField] private string deathAnimationName;
    [SerializeField] private MonoBehaviour[] scriptsToDisable;
    private float currentHealth;

    private void Start()
    {
        var chaos = PlayerPrefs.GetInt("Chaos", 1);
        maxHealth += chaos * 600 - 600;
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage, string dmgType)
    {
        currentHealth -= damage;
        DamagePopupGenerator.instance.RequestDamagePopup(dmgPopupPos.position, damage, dmgType);
        HandleDeath();
        UpdateHpBar();
    }

    public float GetHpAsPercent()
    {
        return currentHealth / maxHealth * 100;
    }

    private void UpdateHpBar()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    private bool isDead;

    private void HandleDeath()
    {
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            StartCoroutine(nameof(Death));
        }
    }

    private IEnumerator Death()
    {
        foreach (var script in scriptsToDisable)
        {
            script.enabled = false;
        }

        animator.Play(deathAnimationName);
        deathEvent.Raise(transform);
        yield return new WaitForSeconds(0.6f);
        Destroy(transform.parent.gameObject);

        if (win != null)
        {
            win.Raise();
        }
    }
}