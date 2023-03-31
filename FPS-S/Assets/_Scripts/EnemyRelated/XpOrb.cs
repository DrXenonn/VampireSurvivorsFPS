using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpOrb : MonoBehaviour
{
    [SerializeField] private float speed;
    public int xpValue;


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Magnet"))
        {
            transform.position = Vector3.Lerp(transform.position, other.transform.position, Time.deltaTime * speed);
        }

        if (other.CompareTag("Collect"))
        {
            Destroy(gameObject);
        }
    }
}
