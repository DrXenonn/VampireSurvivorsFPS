using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    [SerializeField] private float timeToRegen;
    [SerializeField] private float manaToRegenerate;

    private PlayerStats playerStats;
    private float maxMana;
    public float mana = 0;
    private float timeSinceLastRegen = 0f;

    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        maxMana = playerStats.maxMana;
    }

    void Update()
    {
        timeSinceLastRegen += Time.deltaTime;
        if (timeSinceLastRegen >= timeToRegen)
        {
            timeSinceLastRegen -= timeToRegen;
            RegenerateMana();
        }
    }

    void RegenerateMana()
    {
        if (mana < maxMana)
        {
            mana += manaToRegenerate;
            mana = Mathf.Min(mana, maxMana);
        }
    }
}

