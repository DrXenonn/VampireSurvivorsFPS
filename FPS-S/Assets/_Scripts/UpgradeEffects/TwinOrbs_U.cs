using UnityEngine;

public class TwinOrbs_U : MonoBehaviour
{
    [SerializeField] private float twinOrbsPercentage = 0.15f;
    private void Start()
    {
        FindObjectOfType<XpPool>().twinOrbsChance = twinOrbsPercentage;
        Destroy(gameObject);
    }
}
