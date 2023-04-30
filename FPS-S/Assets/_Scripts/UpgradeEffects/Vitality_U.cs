using UnityEngine;

public class Vitality_U : MonoBehaviour
{
    [SerializeField] private int bonusMaxHealth = 10;
    [SerializeField] private GameEvent onMaxHPIncrease;

    void Start()
    {
        onMaxHPIncrease.Raise(bonusMaxHealth);
        Destroy(gameObject);
    }
}
