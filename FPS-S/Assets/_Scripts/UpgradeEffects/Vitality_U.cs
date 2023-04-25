using UnityEngine;

public class Vitality_U : MonoBehaviour
{
    [SerializeField] private int bonusMaxHealth = 10;
    [SerializeField] private GameEvent updageHealthBar;

    void Start()
    {
        updageHealthBar.Raise(bonusMaxHealth);
    }
}
