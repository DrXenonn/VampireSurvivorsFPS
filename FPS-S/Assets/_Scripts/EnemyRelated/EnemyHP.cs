using UnityEngine;
using UnityEngine.AI;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] private EnemyScriptableObject enemyManager;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private GameObject healthBarGameObejct;
    [SerializeField] private SpriteRenderer minimapSprite;
    [SerializeField] private float showHPBarTime;
    private AIManager aiManager;
    private EnemyAnimation enemyAnimation;
    private Animator animator;
    private AIUnit aiUnit;
    private NavMeshAgent navMeshAgent;
    private CapsuleCollider capsuleCollider;
    private GameObject xpPrefab;
    private bool hasXpPrefabSpawned = false;
    [HideInInspector] public int currentEnemyHP;
    private int lastEnemyHP;


    private void Start()
    {
        xpPrefab = enemyManager.xpDropPrefab;
        currentEnemyHP = enemyManager.enemyHealth;
        aiUnit = GetComponent<AIUnit>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        lastEnemyHP = currentEnemyHP;
        animator = GetComponent<Animator>();
        aiManager = GetComponentInParent<AIManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyAnimation = GetComponent<EnemyAnimation>();
    }

    private void Update()
    {
        if (lastEnemyHP != currentEnemyHP)
        {
            lastEnemyHP = currentEnemyHP;
            healthBarGameObejct.SetActive(true);
            Invoke("DisableHealthBar", showHPBarTime);
        }

        healthBar.UpdateHealthbar(enemyManager.enemyHealth, currentEnemyHP);

        if (currentEnemyHP <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        DisableWhenDie();
        Destroy(gameObject, 10f);

        if (!hasXpPrefabSpawned)
        {
            SpawnXpPrefab();
            hasXpPrefabSpawned = true;
        }
    }
    void DisableWhenDie()
    {
        animator.SetTrigger("deathTrigger");
        aiManager.units.Remove(aiUnit);
        navMeshAgent.enabled = false;
        capsuleCollider.enabled = false;
        DisableHealthBar();
        minimapSprite.enabled = false;
        enemyAnimation.enabled = false;
    }

    void SpawnXpPrefab()
    {
        Instantiate(xpPrefab, transform.position, Quaternion.identity);
    }

    void DisableHealthBar()
    {
        healthBarGameObejct.SetActive(false);
    }

}
