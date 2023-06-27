using UnityEngine;

public class LethalForce_U : MonoBehaviour
{
    [SerializeField] private int newCriticalMultiplier;
    private void Awake()
    {
        PlayerStats.Instance.criticalMultiplier = newCriticalMultiplier;
    }
}