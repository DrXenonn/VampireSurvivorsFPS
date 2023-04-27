using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    [SerializeField] private float timeToRegen;
    [SerializeField] private float manaToRegenerate;

    public float maxMana;
    public float currentMana;
    private float timeSinceLastRegen = 0f;

    void Start()
    {
        maxMana = PlayerStats.Instance.maxMana;
        currentMana = maxMana;
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
        if (currentMana < maxMana)
        {
            currentMana += manaToRegenerate;
            currentMana = Mathf.Min(currentMana, maxMana);
        }
    }
}

