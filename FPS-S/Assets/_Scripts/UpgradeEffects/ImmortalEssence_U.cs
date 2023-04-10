using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmortalEssence_U : MonoBehaviour
{
    private PlayerHealth playerHealth;
    [SerializeField] private float bonusMaxHealth = 20f;
    [SerializeField] private float regenThresholdPercent = 50;
    [SerializeField] private float timeToRegen;
    private float timeSinceLastRegen = 0f;

    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerHealth.maxHealth += bonusMaxHealth;
        playerHealth.currentHealth += bonusMaxHealth;
    }

    private void Update()
    {
        timeSinceLastRegen += Time.deltaTime;
        if (timeSinceLastRegen >= timeToRegen)
        {
            timeSinceLastRegen -= timeToRegen;
            playerHealth.RegenerateHealth(regenThresholdPercent);
        }
    }
}
