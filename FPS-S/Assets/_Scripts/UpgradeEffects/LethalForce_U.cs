using UnityEngine;

public class LethalForce_U : MonoBehaviour
{
    [SerializeField] private float newCriticalMultiplier;
    private void Awake()
    {
        PlayerStats.Instance.criticalMultiplier = newCriticalMultiplier;
    }
}