using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    public int maxHealth;
    public float movementSpeed;
    public float projectileSpeed;
    public float fireRate;
    public uint maxMana;
    public int primaryDamage;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /* 
    Crtl + MM --> Close Methods and...;
    Crtl + ,  --> Open File Explorer;
    Crtl + F4 --> Close Tab;
    Crtl + TAB --> Navigate among scripts;
    propfull + TAB --> Create a variable with properties;
    Shift + ArrowUp and Down --> to select a section of code;
    */


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


}
