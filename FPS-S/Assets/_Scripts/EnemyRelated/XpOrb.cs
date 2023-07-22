using System;
using Unity.VisualScripting;
using UnityEngine;

public class XpOrb : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameEvent onCollectXP;
    [SerializeField] private TrailRenderer trailRenderer;
    public int xpValue;

    private void OnEnable()
    {
        trailRenderer.Clear();
        trailRenderer.enabled = false;
        trailRenderer.enabled = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Magnet"))
        {
            transform.position = Vector3.Lerp(transform.position, other.transform.position, Time.deltaTime * speed);
        }

        if (other.CompareTag("Collect"))
        {
            onCollectXP.Raise(this, gameObject);
        }
    }
}