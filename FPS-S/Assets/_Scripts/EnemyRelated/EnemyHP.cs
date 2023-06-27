using System.Collections;
using UnityEngine;

[DefaultExecutionOrder(0)]
public class EnemyHP : MonoBehaviour
{
    [SerializeField] private EnemyScriptableObject enemyManager;
    [SerializeField] private GameEvent onDeath;
    private GameObject xpPrefab;
    private int currentEnemyHp;
    private bool isDead;

    private void Start()
    {
        xpPrefab = enemyManager.xpDropPrefab;
        currentEnemyHp = enemyManager.enemyHealth;
    }

    private void OnEnable()
    {
        currentEnemyHp = enemyManager.enemyHealth;
        isDead = false;
    }

    public void TakeDamage(int damage, bool isCritical)
    {
        currentEnemyHp -= damage;
        DamagePopupGenerator.instance.RequestDamagePopup(GetPosition(), damage, isCritical);
        HandleDeath();
    }

    private float GetHpInPercent()
    {
        return (float)currentEnemyHp / enemyManager.enemyHealth * 100;
    }

    private Vector3 GetPosition()
    {
        var position = transform.position;
        return new Vector3(position.x, transform.localScale.y, position.z);
    }

    private void HandleDeath()
    {
        if (GetHpInPercent() <= PlayerStats.Instance.executionThreshold)
        {
            Death();
        }
    }
    
    private void Death()
    {
        if (isDead)
        {
            Destroy(gameObject);
        }

        isDead = true;
        onDeath.Raise(gameObject.transform, xpPrefab);
        
        if (isActiveAndEnabled)
        { 
            StartCoroutine(nameof(ReleaseDelayed));
        }
    }

    private IEnumerator ReleaseDelayed()
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        EnemySpawner.pool.Release(gameObject);
    }
}