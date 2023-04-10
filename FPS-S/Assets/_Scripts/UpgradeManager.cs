using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public List<UpgradeData> upgrades;
    [HideInInspector] public List<UpgradeData> shownUpgrades;
    public List<UpgradeData> acquiredUpgrades;
    [SerializeField] private XpSystem xpSystem;
    [SerializeField] private UpgradePanelManager upgradePanelManager;


    public List<UpgradeData> GetUpgrades(int count)
    {
        List<UpgradeData> suitableUpgradesList = new List<UpgradeData>();

        if (count > upgrades.Count)
        {
            count = upgrades.Count;
        }

        List<int> selectedIndexes = new List<int>();

        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, upgrades.Count);

            while (selectedIndexes.Contains(randomIndex))
            {
                randomIndex = Random.Range(0, upgrades.Count);
            }

            selectedIndexes.Add(randomIndex);
            suitableUpgradesList.Add(upgrades[randomIndex]);
        }

        return suitableUpgradesList;
    }

    public void Upgrade(int selectedUpgradeID)
    {
        UpgradeData upgradeData = shownUpgrades[selectedUpgradeID];

        if (acquiredUpgrades == null) { acquiredUpgrades = new List<UpgradeData>(); }

        acquiredUpgrades.Add(upgradeData);
        Instantiate(upgradeData.upgradeEffect,gameObject.transform);
        upgrades.AddRange(upgradeData.upgradesToAdd);
        upgrades.RemoveAll(u => upgradeData.upgradesToRemove.Contains(u));
        upgrades.Remove(upgradeData);
    }

    public void ShowUpgrades()
    {
        if (shownUpgrades == null) { shownUpgrades = new List<UpgradeData>(); }
        shownUpgrades.Clear();
        shownUpgrades.AddRange(GetUpgrades(3));
    }
}
