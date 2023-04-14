using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public struct EnemyType
    {
        public GameObject enemyPrefab;
        public Transform spawnParent;
    }

    [System.Serializable]
    public struct Tier
    {
        public string tierName;
        public List<EnemyType> enemyTypes;
        public float spawnRate;
    }

    [SerializeField] private Transform playerTransform;
    [SerializeField] private float minDistance;
    [SerializeField] private int maxEnemies = 10;
    [SerializeField] private List<Tier> tiers;
    [SerializeField] private float timeToIncreaseTier = 60.0f;
    [SerializeField] private BoxCollider spawnArea;

    private Bounds spawnBounds;
    private float timeSinceLastSpawn = 0.0f;
    private int currentEnemies = 0;
    [HideInInspector] public int currentTier = 1;
    private float timeSinceLastTierIncrease = 0.0f;

    void Update()
    {
        if (currentEnemies < maxEnemies)
        {
            timeSinceLastSpawn += Time.deltaTime;

            if (timeSinceLastSpawn >= tiers[currentTier - 1].spawnRate)
            {
                SpawnEnemy(currentTier);
                timeSinceLastSpawn = 0.0f;
            }
        }

        timeSinceLastTierIncrease += Time.deltaTime;
        if (timeSinceLastTierIncrease >= timeToIncreaseTier)
        {
            IncreaseTier();
            timeSinceLastTierIncrease = 0.0f;
        }
    }

    void SpawnEnemy(int currentTier)
    {
        // Get the enemies for the current tier
        List<EnemyType> enemies = tiers[currentTier - 1].enemyTypes;
        spawnBounds = spawnArea.bounds;
        Vector3 spawnPos = new Vector3(Random.Range(spawnBounds.min.x, spawnBounds.max.x), 0, Random.Range(spawnBounds.min.z, spawnBounds.max.z));

        foreach (EnemyType enemy in enemies)
        {
            float distance = Vector3.Distance(playerTransform.position, spawnPos);
            if (distance < minDistance)
            {
                // The spawn position is too close, move it further away from the player
                Vector3 dir = spawnPos - playerTransform.position;
                dir.Normalize();
                spawnPos = playerTransform.position + dir * minDistance;
            }

            Instantiate(enemy.enemyPrefab, spawnPos, Quaternion.identity, enemy.spawnParent);

            currentEnemies++;
            if (currentEnemies >= maxEnemies)
            {
                // Reached max enemies, stop spawning
                enabled = false;
            }
        }
    }

    public void IncreaseTier()
    {
        currentTier++;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(spawnBounds.center, spawnBounds.size);
    }
}

