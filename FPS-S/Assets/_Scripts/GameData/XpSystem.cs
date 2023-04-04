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
    private GameObject upgradePanel;

    void Update()
    {
        if (currentXP >= xpToNextLevel)
        {
            currentXP -= xpToNextLevel;
            currentLevel++;

            xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * xpMultiplier);
            upgradePanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;

        }
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
    }
}
