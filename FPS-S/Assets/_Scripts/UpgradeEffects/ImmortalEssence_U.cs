using UnityEngine;

public class ImmortalEssence_U : MonoBehaviour
{
    [SerializeField] private int bonusMaxHealth = 20;
    [SerializeField] private float timeToRegen;
    [SerializeField] private GameEvent onMaxHPIncrease;
    [SerializeField] private GameEvent onHeal;


    void Start()
    {
        onMaxHPIncrease.Raise(bonusMaxHealth);
        InvokeRepeating("RegenerateHealth", 0, timeToRegen);
    }

    private void RegenerateHealth()
    {
        onHeal.Raise(1);
    }
}
