using UnityEngine;

public class FistOfPower_U : MonoBehaviour
{
    [SerializeField] private int bonusBaseDamagePercent;

    private void Start()
    {
        PlayerStats.Instance.GainBaseDamage(bonusBaseDamagePercent);
        Destroy(gameObject);
    }
}
