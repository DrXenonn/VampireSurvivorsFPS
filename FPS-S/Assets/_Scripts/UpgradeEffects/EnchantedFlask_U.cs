using UnityEngine;

public class EnchantedFlask_U : MonoBehaviour
{
    [SerializeField] private int manaToAdd;

    private void Awake()
    {
        PlayerStats.Instance.maxMana += manaToAdd;
        GameObject.FindWithTag("Player").GetComponent<Mana>().ApplyManaBuff();
    }
}
