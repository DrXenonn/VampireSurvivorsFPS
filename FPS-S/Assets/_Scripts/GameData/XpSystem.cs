using UnityEngine;
using System.Collections;

public class XpSystem : MonoBehaviour
{

    [SerializeField]
    private int currentXP;

    [HideInInspector]
    public int currentLevel;

    [SerializeField]
    private int xpToNextLevel;

    [SerializeField]
    private float xpMultiplier;

    void Update()
    {
        if (currentXP >= xpToNextLevel)
        {
            currentXP -= xpToNextLevel;
            currentLevel++;

            xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * xpMultiplier);
        }
        Debug.Log("Xp: " + currentXP + " / " + xpToNextLevel);
        Debug.Log("Level: " + currentLevel);
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
