using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] private EnemyScriptableObject enemyManager;
    [SerializeField] private SpriteRenderer minimapSprite;
    [SerializeField] private GameEvent onDeath;
    private AIManager aiManager;
    private EnemyAnimation enemyAnimation;
    private Animator animator;
    private AIUnit aiUnit;
    private NavMeshAgent navMeshAgent;
    private BoxCollider boxCollider;
    private GameObject xpPrefab;
    private bool hasDied = false;
    [HideInInspector] public int currentEnemyHP;
    private bool hasInitialized = true;

    private void Start()
    {
        xpPrefab = enemyManager.xpDropPrefab;
        currentEnemyHP = enemyManager.enemyHealth;
        aiUnit = GetComponent<AIUnit>();
        boxCollider = GetComponent<BoxCollider>();
        animator = GetComponent<Animator>();
        aiManager = GetComponentInParent<AIManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyAnimation = GetComponent<EnemyAnimation>();
    }

    private void OnEnable()
    {
        if (!hasInitialized)
        {
            currentEnemyHP = enemyManager.enemyHealth;
            aiManager.units.Add(aiUnit);
            navMeshAgent.enabled = true;
            boxCollider.enabled = true;
            enemyAnimation.enabled = true;
            minimapSprite.enabled = true;
            hasDied = false;
        }
        hasInitialized = false;
    }

    private void Update()
    {
        if (!hasDied && currentEnemyHP <= 0 )
        {
            Death();
        }
    }

    private void Death() 
    {
        StartCoroutine("DisableDelayed");
        DisableWhenDie();
        onDeath.Raise(gameObject.transform, xpPrefab);
        hasDied = true;
    }

    void DisableWhenDie()
    {
        animator.SetTrigger("deathTrigger");
        aiManager.units.Remove(aiUnit);
        navMeshAgent.enabled = false;
        boxCollider.enabled = false;
        minimapSprite.enabled = false;
        enemyAnimation.enabled = false;
    }

    private IEnumerator DisableDelayed()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        EnemySpawner.pool.Release(gameObject);
    }
}
