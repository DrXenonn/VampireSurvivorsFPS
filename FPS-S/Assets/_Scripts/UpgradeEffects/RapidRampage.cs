using System;
using UnityEngine;

public class RapidRampage : MonoBehaviour
{
    [SerializeField] private float bonusFireRatePercent;
    [SerializeField] private float damageReductionPercent;

    private void Start()
    {
        PlayerStats.Instance.GainBonus("fireRate", bonusFireRatePercent);
        PlayerStats.Instance.GainBaseDamage(-damageReductionPercent);
        Destroy(gameObject);
    }
}
