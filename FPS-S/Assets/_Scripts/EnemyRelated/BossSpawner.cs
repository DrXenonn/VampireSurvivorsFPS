using System;
using System.Collections;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [Serializable]
    private struct BossData
    {
        public Transform spawnTransform;
        public float spawnTime;
        public GameObject bossPrefab;
    }

    [SerializeField] private BossData[] bossData;

    private void Awake()
    {
        foreach (var boss in bossData)
        {
            StartCoroutine(SpawnBoss(boss));
        }
    }

    private IEnumerator SpawnBoss(BossData boss)
    {
        yield return new WaitForSeconds(boss.spawnTime);
        Instantiate(boss.bossPrefab, boss.spawnTransform.position, Quaternion.identity);
    }
}
