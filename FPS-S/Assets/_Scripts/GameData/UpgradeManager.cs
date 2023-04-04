using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private int upgradesPerLevel = 3;
    [SerializeField] private int levelIntervalForClassUpgrades = 10;
    [SerializeField] private List<Upgrade> generalUpgrades;
    [SerializeField] private List<Upgrade> fireUpgrades;
    [SerializeField] private List<Upgrade> iceUpgrades;
    [SerializeField] private List<Upgrade> lightningUpgrades;
    [SerializeField] private XpSystem xpSystem;

    private List<Upgrade> availableUpgrades = new List<Upgrade>();

    public delegate void UpgradeEvent(Upgrade upgrade);
    public event UpgradeEvent OnUpgradeSelected;

    private void Start()
    {
        UpgradePlayer();
    }

    public void UpgradePlayer()
    {
        // Clear available upgrades list
        availableUpgrades.Clear();

        // Add general upgrades to available upgrades
        foreach (Upgrade upgrade in generalUpgrades)
        {
            availableUpgrades.Add(upgrade);
        }

        // Add class upgrades to available upgrades if applicable
        if (xpSystem.currentLevel % levelIntervalForClassUpgrades == 0)
        {
            Magic playerMagic = MagicManager.Instance.CurrentMagic;
            switch (playerMagic)
            {
                case Magic.Fire:
                    foreach (Upgrade upgrade in fireUpgrades)
                    {
                        availableUpgrades.Add(upgrade);
                    }
                    break;

                case Magic.Ice:
                    foreach (Upgrade upgrade in iceUpgrades)
                    {
                        availableUpgrades.Add(upgrade);
                    }
                    break;

                case Magic.Lightning:
                    foreach (Upgrade upgrade in lightningUpgrades)
                    {
                        availableUpgrades.Add(upgrade);
                    }
                    break;

                default:
                    break;
            }
        }

        // Randomly select and show available upgrades
        for (int i = 0; i < upgradesPerLevel; i++)
        {
            Upgrade selectedUpgrade = availableUpgrades[Random.Range(0, availableUpgrades.Count)];
            availableUpgrades.Remove(selectedUpgrade);

            // Raise upgrade selected event
            OnUpgradeSelected?.Invoke(selectedUpgrade);
        }
    }
}
