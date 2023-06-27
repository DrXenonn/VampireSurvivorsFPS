using UnityEngine;

public class Mana : MonoBehaviour
{
    [SerializeField] private int manaToRegenerate;

    [HideInInspector] public int maxMana;
    [HideInInspector] public int currentMana;

    private float timeToRegen;

    private void Start()
    {
        maxMana = PlayerStats.Instance.maxMana;
        timeToRegen = PlayerStats.Instance.timeToRegenMana;
        currentMana = maxMana;
        InvokeRepeating(nameof(RegenerateMana), 0, timeToRegen);
    }

    private void RegenerateMana()
    {
        if (currentMana >= maxMana) return;
        currentMana += manaToRegenerate;
        currentMana = Mathf.Min(currentMana, maxMana);
    }

    public void RestoreMana(Component sender, object data)
    {
        currentMana += (int)data;
        currentMana = Mathf.Min(currentMana, maxMana);
    }

    public void ApplyManaBuff()
    {
        timeToRegen = PlayerStats.Instance.timeToRegenMana;
        maxMana = PlayerStats.Instance.maxMana;
        CancelInvoke(nameof(RegenerateMana));
        InvokeRepeating(nameof(RegenerateMana), 0, timeToRegen);
    }
}