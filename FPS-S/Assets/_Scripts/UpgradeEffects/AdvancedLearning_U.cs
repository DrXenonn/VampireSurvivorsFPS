using UnityEngine;

public class AdvancedLearning_U : MonoBehaviour
{
    [SerializeField] private float xpValueMultiplier;

    void Start()
    {
        GameObject.FindGameObjectWithTag("Collect").GetComponent<XpSystem>().xpValueMultiplier += xpValueMultiplier;
        Destroy(gameObject);
    }
}
