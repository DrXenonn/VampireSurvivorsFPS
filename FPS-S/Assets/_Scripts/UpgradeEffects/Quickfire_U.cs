using UnityEngine;

public class Quickfire_U : MonoBehaviour
{
    [SerializeField] private float bonusFireRatePercent;
    [SerializeField] private float bonusProjectileSpeedPercent;

    private void Start()
    {
        PlayerStats.Instance.GainBonus("fireRate", bonusFireRatePercent);
        PlayerStats.Instance.GainBonus("projectileSpeed", bonusProjectileSpeedPercent);
        Destroy(gameObject);
    }
}
