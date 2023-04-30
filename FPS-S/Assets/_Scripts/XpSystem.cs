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

    [HideInInspector]
    public float xpValueMultiplier;

    private void Start()
    {
        xpValueMultiplier = 1f;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.L))
        {
            EarnXP(1000);
        }
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
            EarnXP((int)(other.GetComponent<XpOrb>().xpValue * xpValueMultiplier));
        }
    }

    public void EarnXP(int xpAmount)
    {
        currentXP += xpAmount;
        CheckLevelUp();
        updateUI.Raise();
    }
}
