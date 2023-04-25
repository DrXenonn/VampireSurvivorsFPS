using UnityEngine;
using UnityEngine.AI;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] private EnemyScriptableObject enemyManager;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private GameObject healthBarGameObejct;
    [SerializeField] private SpriteRenderer minimapSprite;
    [SerializeField] private GameEvent onDeath;
    private AIManager aiManager;
    private EnemyAnimation enemyAnimation;
    private Animator animator;
    private AIUnit aiUnit;
    private NavMeshAgent navMeshAgent;
    private CapsuleCollider capsuleCollider;
    private GameObject xpPrefab;
    private bool hasDied = false;
    [HideInInspector] public int currentEnemyHP;


    private void Start()
    {
        xpPrefab = enemyManager.xpDropPrefab;
        currentEnemyHP = enemyManager.enemyHealth;
        aiUnit = GetComponent<AIUnit>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        aiManager = GetComponentInParent<AIManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyAnimation = GetComponent<EnemyAnimation>();
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
        Destroy(gameObject, 0.65f);
        DisableWhenDie();

        if (!hasDied)
        {
            onDeath.Raise(gameObject.transform, xpPrefab);
            hasDied = true;
        }
    }
    void DisableWhenDie()
    {
        animator.SetTrigger("deathTrigger");
        aiManager.units.Remove(aiUnit);
        navMeshAgent.enabled = false;
        capsuleCollider.enabled = false;
        minimapSprite.enabled = false;
        enemyAnimation.enabled = false;
    }
}
