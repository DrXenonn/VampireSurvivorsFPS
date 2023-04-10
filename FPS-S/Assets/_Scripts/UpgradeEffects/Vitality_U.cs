using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitality_U : MonoBehaviour
{
    private PlayerHealth playerHealth;
    [SerializeField] private float bonusMaxHealth = 10f;

    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerHealth.maxHealth += bonusMaxHealth;
        playerHealth.currentHealth += bonusMaxHealth;
    }
}
