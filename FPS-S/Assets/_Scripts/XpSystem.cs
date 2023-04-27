using System;
using System.Collections.Generic;
using UnityEngine;

public class XpSystem : MonoBehaviour
{

    [HideInInspector]
    public int currentXP;

    [HideInInspector]
    public int currentLevel;

    [HideInInspector]
    public int xpToNextLevel;

    [SerializeField]
    private float xpMultiplier;

    [SerializeField]
    private UpgradePanelManager upgradePanelManager;

    [SerializeField]
    private UpgradeManager upgradeManager;

    [SerializeField]
    private GameEvent updateUI;

    void Update()
    {
        CheckLevelUp();
    }

    private void CheckLevelUp()
    {
        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        upgradeManager.ShowUpgrades();

        currentXP -= xpToNextLevel;
        currentLevel++;
        updateUI.Raise();

        xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * xpMultiplier);
        upgradePanelManager.OpenPanel(upgradeManager.shownUpgrades);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("XP"))
        {
            EarnXP(other.GetComponent<XpOrb>().xpValue);
        }
    }

    public void EarnXP(int xpAmount)
    {
        currentXP += xpAmount;
        updateUI.Raise();
    }
}
