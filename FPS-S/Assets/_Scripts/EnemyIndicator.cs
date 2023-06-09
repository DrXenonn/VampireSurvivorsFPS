using System;
using UnityEngine;

public class EnemyIndicator : MonoBehaviour
{
    [SerializeField] private GameObject enemyIndicatorPrefab;
    [SerializeField] private float distanceThreshold;
    [SerializeField] private float angle;
    private Transform playerTransform;
    private Transform parent;
    private GameObject instantiatedIndicator;

    private void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        parent = GameObject.FindWithTag("IndicatorCanvas").transform;

        instantiatedIndicator = Instantiate(enemyIndicatorPrefab, parent);
        instantiatedIndicator.SetActive(false);
    }

    private void Update()
    {
        ShowIndicator();
    }

    private void ShowIndicator()
    {
        if (!IsEnemyCloseEnoughAndBehind())
        {
            instantiatedIndicator.SetActive(false);
            return;
        }
        instantiatedIndicator.SetActive(true);
        instantiatedIndicator.transform.rotation = Quaternion.Euler(0f, 0f, -GetPlayerToEnemyAngle());

    }

    private bool IsEnemyCloseEnoughAndBehind()
    {
        return IsCloseEnough() && GetPlayerToEnemyAngle() >= -angle && GetPlayerToEnemyAngle() <= angle;
    }

    private Vector3 GetPlayersForwardVector()
    {
        return playerTransform.forward;
    }

    private float GetPlayerToEnemyAngle()
    {
        return Vector3.SignedAngle(GetPlayersForwardVector(), GetDirection(), Vector3.up);
    }

    private Vector3 GetDirection()
    {
        return playerTransform.position - transform.position;
    }

    private bool IsCloseEnough()
    {
        var distance = GetDirection().magnitude;
        return distance <= distanceThreshold;
    }

    private void OnDisable()
    {
        instantiatedIndicator.SetActive(false);
    }
}