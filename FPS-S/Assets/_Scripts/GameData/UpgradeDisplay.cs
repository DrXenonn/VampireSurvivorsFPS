using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UpgradeDisplay : MonoBehaviour
{
    public Text nameText;
    public Text descriptionText;
    public Text levelRequirementText;
    public Image iconImage;

    private Upgrade upgrade;

    // Sets the upgrade to be displayed
    public void SetUpgrade(Upgrade upgrade)
    {
        this.upgrade = upgrade;
        nameText.text = upgrade.name;
        descriptionText.text = upgrade.description;
        levelRequirementText.text = "Level " + upgrade.levelRequirement.ToString();
        iconImage.sprite = upgrade.icon;
    }

    // Applies the upgrade
    public void ApplyUpgrade()
    {
        upgrade.ApplyUpgrade();
    }
}
