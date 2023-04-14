using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LastStandAoE : MonoBehaviour
{

    [SerializeField] private float expansionDuration = 2f;
    [SerializeField] private int blastDamage = 1000;
    [SerializeField] private float finalRadius = 100f;
    [SerializeField] private string[] affectedTags;
    private float currentRadius = 0f;
    private List<Collider> affectedColliders = new List<Collider>();
    private LineRenderer lineRenderer;
    private SphereCollider xpMagnet;

    private void Start()
    {
        xpMagnet = GameObject.FindGameObjectWithTag("Magnet").GetComponent<SphereCollider>();
        lineRenderer = GetComponent<LineRenderer>();
        xpMagnet.enabled = false;
        lineRenderer.enabled = false;
        StartCoroutine(Expand());
    }

    IEnumerator Expand()
    {
        float radiusIncreasePerSecond = finalRadius / expansionDuration;
        lineRenderer.enabled = true;
        while (currentRadius < finalRadius)
        {
            currentRadius += radiusIncreasePerSecond * Time.deltaTime;
            transform.localScale = new Vector3(currentRadius, currentRadius, currentRadius);
            Collider[] colliders = Physics.OverlapSphere(transform.position, currentRadius);
            foreach (Collider collider in colliders)
            {
                if (!affectedColliders.Contains(collider) && System.Array.IndexOf(affectedTags, collider.tag) >= 0)
                {
                    EnemyHP health = collider.gameObject.GetComponent<EnemyHP>();
                    if (health != null)
                    {
                        health.currentEnemyHP -= blastDamage;
                    }
                    affectedColliders.Add(collider);
                }
            }
            DrawCircle();
            yield return null;
        }
        lineRenderer.enabled = false;
        xpMagnet.enabled = true;
        Destroy(gameObject);
    }

    private void DrawCircle()
    {
        float theta = 0f;
        float thetaScale = 0.1f;
        int size = (int)((2.0 * Mathf.PI) / thetaScale) + 1;
        lineRenderer.positionCount = size;

        for (int i = 0; i < size; i++)
        {
            theta += (2.0f * Mathf.PI * thetaScale);
            float x = currentRadius * Mathf.Cos(theta);
            float z = currentRadius * Mathf.Sin(theta);
            lineRenderer.SetPosition(i, new Vector3(x, 0, z) + transform.position);
        }
    }
}
