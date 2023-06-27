using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[DefaultExecutionOrder(1)]
public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public struct Tier
    {
        public string tierName;
        public EnemyType enemyType;
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
    [SerializeField] private GameEvent onSpawn;
    
    private float timeSinceLastSpawn = 0.0f;
    private int currentTier = 1;
    private float timeSinceLastTierIncrease = 0.0f;
    public static ObjectPool<GameObject> pool;

    private void Start()
    {
        pool = new ObjectPool<GameObject>(
            createFunc: () => SpawnEnemy(),
            actionOnGet: (obj) => EnableEnemy(),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: 75,
            maxSize: 300
        );
    }

    private void Update()
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

    private GameObject SpawnEnemy()
    {
        EnemyType enemyType = tiers[currentTier - 1].enemyType;
        var spawnPos = GetARandomPosition();
        var enemy = Instantiate(enemyType.enemyPrefab, spawnPos, Quaternion.identity, enemyType.spawnParent);
        onSpawn.Raise(enemy.transform);

        return enemy;
    }

    private void EnableEnemy()
    {
        EnemyType enemyType = tiers[currentTier - 1].enemyType;
        List<GameObject> disabledChildren = new List<GameObject>();

        // Find disabled children of the spawn parent with the matching enemy type
        foreach (Transform child in enemyType.spawnParent)
        {
            if (!child.gameObject.activeSelf)
            {
                disabledChildren.Add(child.gameObject);
            }
        }

        // Enable the first disabled child with the matching enemy type
        if (disabledChildren.Count > 0)
        {
            var spawnPos = GetARandomPosition();
            GameObject disabledEnemy = disabledChildren[0];
            disabledEnemy.SetActive(true);
            disabledEnemy.transform.position = spawnPos;
            onSpawn.Raise(disabledEnemy.transform);
        }
    }

    private Vector3 GetARandomPosition()
    {
        var spawnPos = Vector3.zero;
        var randomDirection = Random.insideUnitSphere.normalized;

        var bounds = spawnArea.bounds;
        spawnPos.x = bounds.center.x + randomDirection.x * bounds.extents.x;
        spawnPos.z = bounds.center.z + randomDirection.z * bounds.extents.z;
        spawnPos.y = 0;

        var distanceToPlayer = Vector3.Distance(spawnPos, playerTransform.position);

        if (distanceToPlayer <= minDistance)
        {
            var directionToCenter = (bounds.center - spawnPos).normalized;
            var maxDistance = bounds.extents.magnitude - minDistance;
            spawnPos = bounds.center + directionToCenter * maxDistance;
        }

        return spawnPos;
    }

    private void IncreaseTier()
    {
        currentTier++;
    }
}