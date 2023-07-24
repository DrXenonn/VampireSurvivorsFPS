using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossHP : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private GameEvent deathEvent;
    [SerializeField] private Transform dmgPopupPos;
    [SerializeField] private Animator animator;
    [SerializeField] private BossMovement bossMovement;
    [SerializeField] private DragonBehaviour dragonBehaviour;
    [SerializeField] private Image healthBar;
    private float currentHealth;
    private int phase;

    private void Start()
    {
        phase = 1;
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage, string dmgType)
    {
        currentHealth -= damage;
        DamagePopupGenerator.instance.RequestDamagePopup(dmgPopupPos.position ,damage, dmgType);
        HandleDeath();
        UpdateHpBar();
        CheckChangePhase();
    }

    private void CheckChangePhase()
    {
        if ((currentHealth / maxHealth) < 0.5f && phase == 1)
        {
            dragonBehaviour.ChangePhase();
            phase = 2;
        }
        else if((currentHealth / maxHealth) < 0.25f && phase == 2)
        {
            dragonBehaviour.ChangePhase();
            phase = 3;
        }
    }

    private void UpdateHpBar()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    private void HandleDeath()
    {
        if (currentHealth <= 0)
        {
            StartCoroutine(nameof(Death));
        }
    }

    private IEnumerator Death()
    {
        dragonBehaviour.enabled = false;
        bossMovement.enabled = false;
        animator.Play("DragonArmature|Dragon_Death");
        deathEvent.Raise(transform);
        yield return new WaitForSeconds(0.6f);
        Destroy(transform.parent.gameObject);
    }
}
