using System;
using UnityEngine;

public class Killspire_U : MonoBehaviour
{
    private EnemyCounter enemyCounter;

    private void Start()
    {
        enemyCounter = FindObjectOfType<EnemyCounter>();
        
        PlayerStats.Instance.primaryDamage += enemyCounter.killedEnemies;
    }

    public void OnKill(Component sender, object data)
    {
        PlayerStats.Instance.primaryDamage += 1;
    }
}
