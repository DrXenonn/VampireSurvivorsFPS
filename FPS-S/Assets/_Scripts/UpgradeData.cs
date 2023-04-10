using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade/New")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    public Sprite upgradeIcon;
    public Color upgradeColor;
    public GameObject upgradeEffect;
    public GameObject upgradeUI;
    public List<UpgradeData> upgradesToAdd;
    public List<UpgradeData> upgradesToRemove;
    public int requiredLevel;
}
