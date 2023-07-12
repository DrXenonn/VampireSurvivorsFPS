using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BerserkMode : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float berserkModeTime;
    [SerializeField] private RawImage icon;
    private float normalSpeed;

    private void Start()
    {
        normalSpeed = agent.speed;
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(nameof(GoBerserk));
    }
    
    public void BecomeNormal() 
    {
        agent.speed = normalSpeed;
        icon.enabled = false;
        StopAllCoroutines();
        StartCoroutine(nameof(GoBerserk));
    }

    private IEnumerator GoBerserk()
    {
        yield return new WaitForSeconds(berserkModeTime);
        icon.enabled = true;
        yield return new WaitForSeconds(15f);
        agent.speed = PlayerStats.Instance.movementSpeed + 5f;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}