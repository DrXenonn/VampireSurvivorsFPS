using UnityEngine;

public class LastStand_U : MonoBehaviour
{
    private PlayerHealth playerHealth;
    [SerializeField] private float bonusMaxHealth = 10f;
    [SerializeField] private float invincibilityTime = 5f;
    [SerializeField] private float cooldownTime = 300f;

    private float remainingCooldownTime = 0f;

    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerHealth.maxHealth += bonusMaxHealth;
        playerHealth.currentHealth += bonusMaxHealth;
        playerHealth.canDie = false;
    }

    private void Update()
    {
        if (playerHealth.currentHealth <= 0 && remainingCooldownTime <= 0)
        {
            playerHealth.currentHealth = Mathf.RoundToInt(playerHealth.maxHealth / 2);
            playerHealth.BecomeInvincible(invincibilityTime);
            Invoke("DisableInvincibility", invincibilityTime + 0.01f);

            remainingCooldownTime = cooldownTime;
        }

        if (remainingCooldownTime > 0f)
        {
            remainingCooldownTime -= Time.deltaTime;
        }
    }


    private void DisableInvincibility()
    {
        playerHealth.canDie = true;
    }
}
