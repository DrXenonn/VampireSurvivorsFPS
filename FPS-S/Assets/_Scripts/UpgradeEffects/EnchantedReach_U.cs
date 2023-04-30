using UnityEngine;

public class EnchantedReach_U : MonoBehaviour
{
    private SphereCollider magnetCollider;
    [SerializeField] private float bonusRadiusPercent;
    private void Start()
    {
        magnetCollider = GameObject.FindGameObjectWithTag("Magnet").GetComponent<SphereCollider>();
        magnetCollider.radius *= ((bonusRadiusPercent + 100) / 100);
        Destroy(gameObject);
    }
}
