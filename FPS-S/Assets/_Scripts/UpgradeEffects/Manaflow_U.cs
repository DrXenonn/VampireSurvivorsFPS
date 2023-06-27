using UnityEngine;

public class Manaflow_U : MonoBehaviour
{
    [SerializeField] private float percentReduction;

    private void Awake()
    {
        PlayerStats.Instance.GainBonus("timeToRegenMana", -percentReduction);
        GameObject.FindWithTag("Player").GetComponent<Mana>().ApplyManaBuff();
    }
}