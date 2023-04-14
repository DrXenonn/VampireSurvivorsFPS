using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; set; }

    public int maxHealth;
    public float movementSpeed;
    public float dashDistance;
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

    public void GainBonus<T>(string fieldName, T bonusPercentage) where T : struct, IConvertible
    {
        var field = typeof(PlayerStats).GetField(fieldName);
        if (field != null && field.FieldType == typeof(float))
        {
            var value = (float)field.GetValue(this);
            value += value * (bonusPercentage.ToSingle(null) / 100f);
            field.SetValue(this, value);
        }
        else if (field != null && field.FieldType == typeof(int))
        {
            var value = (int)field.GetValue(this);
            value += (int)(value * (bonusPercentage.ToSingle(null) / 100f));
            field.SetValue(this, value);
        }
    }
}
