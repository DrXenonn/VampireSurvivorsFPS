using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public List<UpgradeData> upgrades;
    [HideInInspector] public List<UpgradeData> shownUpgrades;
    public List<UpgradeData> acquiredUpgrades;

    [SerializeField] private Transform iconParent;
    [SerializeField] private GameObject iconPrefab;
    [SerializeField] private float iconSpacingX = 10f;
    [SerializeField] private float iconSpacingY = 20f;
    [SerializeField] private int iconsPerRow = 6;
    private List<GameObject> upgradeIcons = new List<GameObject>();
    private List<UpgradeData> GetUpgrades(int count)
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
        ShowIconOnTop(upgradeData);
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

    private void ShowIconOnTop(UpgradeData upgrade)
    {
        var newIcon = Instantiate(iconPrefab, iconParent);
        var image = newIcon.GetComponent<Image>();
        image.sprite = upgrade.upgradeIcon;
        image.color = upgrade.upgradeColor;
        upgradeIcons.Add(newIcon);
        var iconIndex = upgradeIcons.Count - 1;
        var offsetX = (iconIndex % iconsPerRow) * iconSpacingX;
        var offsetY = Mathf.Floor(iconIndex / iconsPerRow) * -iconSpacingY;
        newIcon.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetX, offsetY);

    }
}
