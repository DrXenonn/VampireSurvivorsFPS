using UnityEngine;

public class ElectroChain_U : MonoBehaviour
{
    [SerializeField] private int maxElectrifiedEnemies;

    private void Start()
    {
        GameObject.FindWithTag("Electromancy").GetComponent<Electromancy_U>().numberOfClosestEnemies = maxElectrifiedEnemies;
        Destroy(gameObject);
    }
}