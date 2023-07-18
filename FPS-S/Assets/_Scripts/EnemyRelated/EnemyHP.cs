using System;
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
    [HideInInspector] public int bonusHealth;
    [HideInInspector] public bool isDeBuffApplied;

    private void Start()
    {
        xpPrefab = enemyManager.xpDropPrefab;
        currentEnemyHp = enemyManager.enemyHealth + bonusHealth;
    }

    private void OnEnable()
    {
        currentEnemyHp = enemyManager.enemyHealth;
        isDead = false;
        isDeBuffApplied = false;
    }

    public void TakeDamage(int damage, string dmgType = "")
    {
        currentEnemyHp -= damage;
        DamagePopupGenerator.instance.RequestDamagePopup(GetPosition(), damage, dmgType);
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
            return;
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

    private void OnDisable()
    {
        isDeBuffApplied = false;
    }
}