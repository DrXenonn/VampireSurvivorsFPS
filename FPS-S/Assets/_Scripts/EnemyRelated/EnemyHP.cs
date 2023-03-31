using UnityEngine;
using UnityEngine.UI;
public class EnemyHP : MonoBehaviour
{
    [SerializeField] private EnemyScriptableObject enemyManager;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private GameObject healthBarGameObejct;
    [SerializeField] private GameObject enemyBody;
    private AIUnit aiUnit;
    private CapsuleCollider capsuleCollider;
    private GameObject xpPrefab;
    private bool hasXpPrefabSpawned = false;
    [HideInInspector] public int currentEnemyHP;


    private void Start()
    {
        xpPrefab = enemyManager.xpDropPrefab;
        currentEnemyHP = enemyManager.enemyHealth;
        aiUnit = GetComponent<AIUnit>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        if (enemyManager.enemyHealth != currentEnemyHP)
        {
            healthBarGameObejct.SetActive(true);
        }
        healthBar.UpdateHealthbar(enemyManager.enemyHealth, currentEnemyHP);
        if (currentEnemyHP <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        aiUnit.enabled = false;
        capsuleCollider.enabled = false;
        healthBarGameObejct.SetActive(false);
        enemyBody.SetActive(false);
        Destroy(gameObject, 1.5f);

        if (!hasXpPrefabSpawned)
        {
            SpawnXpPrefab();
            hasXpPrefabSpawned = true;
        }
    }

    void SpawnXpPrefab()
    {
        Instantiate(xpPrefab, transform.position, Quaternion.identity);
    }
}
