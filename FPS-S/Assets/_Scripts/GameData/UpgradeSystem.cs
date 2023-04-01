using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    public XpSystem xpSystem;
    private List<UpgradeData> availableUpgrades;
    private List<UpgradeData> pickedUpgrades = new List<UpgradeData>();

    private void Start()
    {
        SetAvailableUpgrades(xpSystem.currentLevel);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log(GetAvailableUpgrades().Count);
        }
    }

    public void SetAvailableUpgrades(int level)
    {
        availableUpgrades = new List<UpgradeData>();
    }

    public List<UpgradeData> GetAvailableUpgrades()
    {
        return availableUpgrades;
    }

    public void PickUpgrade(UpgradeData upgrade)
    {
        pickedUpgrades.Add(upgrade);
    }
}
