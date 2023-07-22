using System.Collections;
using UnityEngine;

public class AoEDamage : MonoBehaviour
{
    [SerializeField] private int damage;
    
    [SerializeField] private GameEvent onDealDamage;
    private bool isPlayerInside;

    private IEnumerator Start()
    {
        var position = transform.position;
        position = new Vector3(position.x, 0.0001f, position.z);
        transform.position = position;
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        isPlayerInside = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }

    private void OnDisable()
    {
        if (isPlayerInside)
        {
            onDealDamage.Raise(damage);
        }
    }
}
