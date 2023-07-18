using UnityEngine;

public class FrozenImpact_U : MonoBehaviour
{
    private void Start()
    {
        GameObject.FindWithTag("Cryomancy").GetComponent<Cryomancy_U>().hasFrozenImpact = true;
        Destroy(gameObject);
    }
}
