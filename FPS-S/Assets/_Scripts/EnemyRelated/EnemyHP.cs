using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] private EnemyScriptableObject enemyManager;
    [SerializeField] private GameEvent onDeath;
    private GameObject xpPrefab;
    private int currentEnemyHp;

    private void Start()
    {
        xpPrefab = enemyManager.xpDropPrefab;
        currentEnemyHp = enemyManager.enemyHealth;
    }

    private void OnEnable()
    {
        currentEnemyHp = enemyManager.enemyHealth;
    }

    public void TakeDamage(int damage)
    {
        currentEnemyHp -= damage;
        DamagePopupGenerator.instance.RequestDamagePopup(GetPosition(), damage);
        HandleDeath();
    }

    private Vector3 GetPosition()
    {
        var position = transform.position;
        return new Vector3(position.x, transform.localScale.y, position.z);
    }

    private void HandleDeath()
    {
        if (currentEnemyHp <= 0)
        {
            EnemySpawner.pool.Release(gameObject);
            onDeath.Raise(gameObject.transform, xpPrefab);
        }
    }
}