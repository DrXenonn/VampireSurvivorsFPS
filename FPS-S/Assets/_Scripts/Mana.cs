using UnityEngine;

public class Mana : MonoBehaviour
{
    [SerializeField] private float timeToRegen;
    [SerializeField] private int manaToRegenerate;

    public int maxMana;
    public int currentMana;

    void Start()
    {
        maxMana = PlayerStats.Instance.maxMana;
        currentMana = maxMana;
        InvokeRepeating("RegenerateMana", 0, timeToRegen);
    }

    void RegenerateMana()
    {
        if (currentMana < maxMana)
        {
            currentMana += manaToRegenerate;
            currentMana = Mathf.Min(currentMana, maxMana);
        }
    }

    public void RestoreMana(Component sender, object data)
    {
        currentMana += (int)data;
        currentMana = Mathf.Min(currentMana, maxMana);
    }
}

