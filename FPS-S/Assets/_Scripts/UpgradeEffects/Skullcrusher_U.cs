using UnityEngine;

public class Skullcrusher_U : MonoBehaviour
{
    [SerializeField] private float bonusCriticalChance;

    private void Awake()
    {
        PlayerStats.Instance.criticalChance += bonusCriticalChance;
    }
}
