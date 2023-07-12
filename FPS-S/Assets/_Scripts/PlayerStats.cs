using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    //Player's stats; changeable anytime except mana and HP.
    public int maxHealth;
    public float movementSpeed;
    public float projectileSpeed;
    public float fireRate;
    public int maxMana;
    public float timeToRegenMana;
    public int primaryDamage;
    public float criticalChance;
    public float criticalMultiplier;
    public float executionThreshold;

    [HideInInspector]public int baseAttackDamage;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
        ApplyAugments();
        baseAttackDamage = primaryDamage;
    }


    //Function to apply buff and de-buff, Also returning the value in order to subtract it later. 
    public float GainBonus<T>(string fieldName, T bonusPercentage) where T : struct, IConvertible
    {
        var field = typeof(PlayerStats).GetField(fieldName);
        if (field != null && field.FieldType == typeof(float))
        {
            var value = (float)field.GetValue(this);
            var multiplier = bonusPercentage.ToSingle(null) / 100f;
            var bonus = value * multiplier;
            value *= (1f + multiplier);
            field.SetValue(this, value);
            return bonus;
        }
        else if (field != null && field.FieldType == typeof(int))
        {
            var value = (int)field.GetValue(this);
            var multiplier = bonusPercentage.ToSingle(null) / 100f;
            var bonus = value * multiplier;
            value = Mathf.RoundToInt(value * (1f + multiplier));
            field.SetValue(this, value);
            return bonus;
        }

        return 0f;
    }

    public void GainBaseDamage(float bonusPercentage)
    {
        var baseValue = baseAttackDamage;
        var multiplier = bonusPercentage / 100f;
        var bonus = baseValue * multiplier;
        primaryDamage += Mathf.RoundToInt(bonus);
    }

    private void ApplyAugments()
    {
        primaryDamage += PlayerPrefs.GetInt("DamageAugment");
        criticalChance += (float)PlayerPrefs.GetInt("CriticalChanceAugment") / 100;
        fireRate += PlayerPrefs.GetInt("FireRateAugment") / 14f;
        timeToRegenMana -= ((float)PlayerPrefs.GetInt("ManaRegenAugment") / 1000);
    }
}