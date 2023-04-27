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
    [SerializeField] private BoxCollider spawnArea;

    private Bounds spawnBounds;
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
        if (timeSinceLastSpawn >= tiers[currentTier - 1].spawnRate)
        {
            pool.Get();
            timeSinceLastSpawn = 0.0f;
        }

        timeSinceLastTierIncrease += Time.deltaTime;
        if (timeSinceLastTierIncrease >= tiers[currentTier - 1].timeToIncreaseTier)
        {
            IncreaseTier();
            timeSinceLastTierIncrease = 0.0f;
        }
    }

    private GameObject SpawnEnemy(int currentTier)
    {
        GameObject obj = null;
        List<EnemyType> enemies = tiers[currentTier - 1].enemyTypes;
        spawnBounds = spawnArea.bounds;

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
        spawnBounds = spawnArea.bounds;

        foreach (EnemyType enemy in enemies)
        {
            Vector3 spawnPos = GenerateRandomSpawnPosition();

            obj.SetActive(true);
            obj.transform.position = spawnPos;
        }
    }

    Vector3 GenerateRandomSpawnPosition()
    {
        Vector3 spawnPos = new Vector3(Random.Range(spawnBounds.min.x, spawnBounds.max.x), 0, Random.Range(spawnBounds.min.z, spawnBounds.max.z));

        float distance = Vector3.Distance(playerTransform.position, spawnPos);
        if (distance < minDistance)
        {
            Vector3 dir = spawnPos - playerTransform.position;
            dir.Normalize();
            spawnPos = playerTransform.position + dir * minDistance;
        }

        return spawnPos;
    }

    public void IncreaseTier()
    {
        currentTier++;
    }
}
