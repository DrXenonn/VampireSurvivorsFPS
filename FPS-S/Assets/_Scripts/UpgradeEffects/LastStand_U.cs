using UnityEngine;

public class LastStand_U : MonoBehaviour
{
    private PlayerHealth playerHealth;
    [SerializeField] private float invincibilityTime = 5f;
    [SerializeField] private int lastChanceHealth = 20;
    [SerializeField] private int bonusDamagePercent = 5;
    [SerializeField] private float bonusFireRatePercent = 15f;
    [SerializeField] private GameEvent onHeal;
    [SerializeField] GameObject aoeEffect;
    [SerializeField] private GameEvent onBuffGain;


    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void GainBonuses()
    {
        PlayerStats.Instance.GainBonus("primaryDamage", bonusDamagePercent);
        PlayerStats.Instance.GainBonus("fireRate", bonusFireRatePercent);
    }

    public void OnFirstDeath(Component sender, object data) 
    {
        playerHealth.BecomeInvincible(invincibilityTime);
        onHeal.Raise(lastChanceHealth);
        Instantiate(aoeEffect, playerHealth.gameObject.transform);
        GainBonuses();
        onBuffGain.Raise();
        Destroy(gameObject, invincibilityTime + 0.1f);
    }
}
