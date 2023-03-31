using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    public XpSystem xpSystem;
    public ClassHandler classHandler;
    private List<UpgradeData> availableUpgrades;
    private List<UpgradeData> pickedUpgrades = new List<UpgradeData>();

    private void Start()
    {
        SetAvailableUpgrades(xpSystem.currentLevel, classHandler);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log(GetAvailableUpgrades().Count);
        }
    }

    public void SetAvailableUpgrades(int level, ClassHandler playerClass)
    {
        List<UpgradeData> classUpgrades = playerClass.chosenClass.upgrades;

        availableUpgrades = new List<UpgradeData>();
        foreach (UpgradeData upgrade in classUpgrades)
        {
            if (!pickedUpgrades.Contains(upgrade))
            {
                availableUpgrades.Add(upgrade);
            }
        }

        while (availableUpgrades.Count < 3)
        {
            int randomIndex = Random.Range(0, classUpgrades.Count);
            availableUpgrades.Add(classUpgrades[randomIndex]);
        }
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
