using System.Collections.Generic;
using UnityEngine;

public static class UpgradeDatabase
{
    private static List<Upgrade> allUpgrades = new List<Upgrade>();

    // Initialize all upgrades
    static UpgradeDatabase()
    {
        allUpgrades.Add(new FireDamageUpgrade());
        allUpgrades.Add(new IceSlowUpgrade());
        // Add more upgrades as needed
    }

    // Returns a list of all upgrades
    public static List<Upgrade> GetAllUpgrades()
    {
        return allUpgrades;
    }

    public class FireDamageUpgrade : Upgrade
    {
        public float damageIncrease;

        public FireDamageUpgrade()
        {
            name = "Fire Damage";
            description = "Increases fire damage by 10%.";
            levelRequirement = 5;
            icon = Resources.Load<Sprite>("FireIcon");
            damageIncrease = 0.1f;
        }

        public override void ApplyUpgrade()
        {
            Debug.Log("Fire Damage Upgrade");
        }
    }

    public class IceSlowUpgrade : Upgrade
    {
        public float slowAmount;

        public IceSlowUpgrade()
        {
            name = "Ice Slow";
            description = "Slows enemies hit by ice attacks by 25%.";
            levelRequirement = 10;
            icon = Resources.Load<Sprite>("IceIcon");
            slowAmount = 0.25f;
        }

        public override void ApplyUpgrade()
        {
            Debug.Log("Ice Slow Upgrade");

        }
    }
}
