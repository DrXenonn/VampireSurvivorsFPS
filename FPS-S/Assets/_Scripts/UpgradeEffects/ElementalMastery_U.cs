using UnityEngine;

public class ElementalMastery_U : MonoBehaviour
{
    [SerializeField] private float newElementalMultiplier;

    private void Start()
    {
        PlayerStats.Instance.elementalDamageMultiplier = newElementalMultiplier;
        Destroy(gameObject);
    }
}
