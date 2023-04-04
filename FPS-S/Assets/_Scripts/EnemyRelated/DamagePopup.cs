using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] private float timeToLive;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 randomizePositionIntensity;
    [SerializeField] private float multiplier = 1f;
    [SerializeField] private MeshRenderer meshRenderer;

    private float distance;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        StartCoroutine(WaitToDisplay(0.1f));
        Destroy(gameObject, timeToLive);
        transform.localPosition += offset;
        transform.localPosition += new Vector3(
            RandomizePosition(randomizePositionIntensity.x),
            Random.Range(0, randomizePositionIntensity.y),
            RandomizePosition(randomizePositionIntensity.z));
    }

    private void Update()
    {
        distance = Vector3.Distance(transform.position, cam.transform.position) * multiplier;
        if (distance < 2f)
        {
            gameObject.transform.localScale = new Vector3(distance * 2f, distance * 2f, distance * 2f);
        }
        else 
        {
            gameObject.transform.localScale = new Vector3(distance, distance, distance);
        }
    }

    private float RandomizePosition(float xyz)
    {
        var randomPos = Random.Range(-xyz, xyz);
        return randomPos;
    }

    IEnumerator WaitToDisplay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        meshRenderer.enabled = true;
    }
}