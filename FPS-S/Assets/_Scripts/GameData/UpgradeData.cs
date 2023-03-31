using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType
{
    WeaponUpgrade,
    MovementUpgrade,
    PassiveUpgrade,
    HealthUpgrade,
    ClassUpgrade
}

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrade", order = 2)]
public class UpgradeData : ScriptableObject
{
    public UpgradeType upgradeType;
    public string upgradeName;
    public Sprite upgradeIcon;
}
