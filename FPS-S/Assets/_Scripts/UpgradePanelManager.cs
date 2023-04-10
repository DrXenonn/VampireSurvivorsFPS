using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanelManager : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    PauseManager pauseManager;

    [SerializeField] UpgradeManager upgradeManager;
    [SerializeField] List<UpgradeButton> upgradeButtons;
    [SerializeField] private XpSystem xpSystem;
    [SerializeField] private Transform upgradeInfo;
    public int selectedID = 0;
    private void Start()
    {
        panel.SetActive(false);
        HideButtons();
        pauseManager = GetComponent<PauseManager>();
    }

    public void OpenPanel(List<UpgradeData> upgradeData)
    {
        Clean();
        SelectUpgrade(0);
        pauseManager.PauseGame();
        panel.SetActive(true);

        for (int i = 0; i < upgradeData.Count; i++)
        {
            upgradeButtons[i].gameObject.SetActive(true);
            upgradeButtons[i].Set(upgradeData[i]);
        }
    }

    public void Clean()
    {
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].Clean();
        }
    }

    public void SelectUpgrade(int pressedButtonID)
    {
        if (upgradeInfo.childCount > 0)
        {
            Transform firstChild = upgradeInfo.GetChild(0);
            Destroy(firstChild.gameObject);
        }
        upgradeButtons[selectedID].RemoveSelectEffect();
        selectedID = pressedButtonID;
        upgradeButtons[pressedButtonID].ApplySelectEffect();
        Instantiate(upgradeManager.shownUpgrades[pressedButtonID].upgradeUI, upgradeInfo);
    }
    public void ApplyUpgrade()
    {
        upgradeManager.Upgrade(selectedID);

        if (xpSystem.currentLevel - 1 != upgradeManager.acquiredUpgrades.Count)
        {
            upgradeManager.ShowUpgrades();
            OpenPanel(upgradeManager.shownUpgrades);
        }
    }

    public void ClosePanel()
    {
        HideButtons();

        panel.SetActive(false);
        pauseManager.UnPauseGame();
    }

    private void HideButtons()
    {
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].gameObject.SetActive(false);
        }
    }
}
