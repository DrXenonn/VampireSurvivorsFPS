using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public struct Tier
    {
        public string tierName;
        public List<EnemyType> enemyTypes;
        public float spawnRate;
        public float timeToIncreaseTier;
    }

    [System.Serializable]
    public struct EnemyType
    {
        public GameObject enemyPrefab;
        public Transform spawnParent;
    }

    [SerializeField] private Transform playerTransform;
    [SerializeField] private float minDistance;
    [SerializeField] private List<Tier> tiers;
    [SerializeField] private SphereCollider spawnArea;

    private float timeSinceLastSpawn = 0.0f;
    [HideInInspector] public int currentTier = 1;
    private float timeSinceLastTierIncrease = 0.0f;
    public static ObjectPool<GameObject> pool;

    private void Start()
    {
        pool = new ObjectPool<GameObject>(
            createFunc: () => SpawnEnemy(currentTier),
            actionOnGet: (obj) => EnableEnemy(obj),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: 75,
            maxSize: 300
        );
    }

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        timeSinceLastTierIncrease += Time.deltaTime;

        // Check if it's time to spawn an enemy
        if (timeSinceLastSpawn >= tiers[currentTier - 1].spawnRate)
        {
            pool.Get();
            timeSinceLastSpawn = 0.0f;
        }

        // Check if it's time to increase the tier
        if (timeSinceLastTierIncrease >= tiers[currentTier - 1].timeToIncreaseTier)
        {
            IncreaseTier();
            timeSinceLastTierIncrease = 0.0f;

            // Spawn an enemy immediately after changing tier
            pool.Get();
            timeSinceLastSpawn = 0.0f;
        }
    }

    private GameObject SpawnEnemy(int currentTier)
    {
        GameObject obj = null;
        List<EnemyType> enemies = tiers[currentTier - 1].enemyTypes;

        foreach (EnemyType enemy in enemies)
        {
            Vector3 spawnPos = GenerateRandomSpawnPosition();
            obj = Instantiate(enemy.enemyPrefab, spawnPos, Quaternion.identity, enemy.spawnParent) as GameObject;
        }

        return obj;
    }

    void EnableEnemy(GameObject obj)
    {
        List<EnemyType> enemies = tiers[currentTier - 1].enemyTypes;

        foreach (EnemyType enemy in enemies)
        {
            // Check if the game object's prefab belongs to the current tier
            if (enemy.enemyPrefab == obj)
            {
                Vector3 spawnPos = GenerateRandomSpawnPosition();
                obj.SetActive(true);
                obj.transform.position = spawnPos;
                return; // Exit the method once we've enabled the game object
            }
        }
    }

    Vector3 GenerateRandomSpawnPosition()
    {
        Vector3 spawnPos = Vector3.zero;
        Vector3 randomDirection = Random.insideUnitSphere.normalized;

        spawnPos.x = spawnArea.bounds.center.x + randomDirection.x * spawnArea.bounds.extents.x; // Calculate the x-coordinate within the spawn area bounds
        spawnPos.z = spawnArea.bounds.center.z + randomDirection.z * spawnArea.bounds.extents.z; // Calculate the z-coordinate within the spawn area bounds
        spawnPos.y = 0;

        float distanceToPlayer = Vector3.Distance(spawnPos, playerTransform.position);

        if (distanceToPlayer <= minDistance)
        {
            Vector3 directionToCenter = (spawnArea.bounds.center - spawnPos).normalized;
            float maxDistance = spawnArea.bounds.extents.magnitude - minDistance;
            spawnPos = spawnArea.bounds.center + directionToCenter * maxDistance;
        }

        return spawnPos;
    }



    public void IncreaseTier()
    {
        currentTier++;
    }
}