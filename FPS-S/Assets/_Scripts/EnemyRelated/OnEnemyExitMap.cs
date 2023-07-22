using UnityEngine;


public class OnEnemyExitMap : MonoBehaviour
{
    [HideInInspector] public bool isEnemyInside = true;
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Knockback"))
        {
            isEnemyInside = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Knockback"))
        {
            isEnemyInside = true;
        }
    }
}
