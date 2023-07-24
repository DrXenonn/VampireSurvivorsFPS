using System;
using UnityEngine;

public class Killspire_U : MonoBehaviour
{
    [SerializeField] private int stacksForOneDamage;
    private EnemyCounter enemyCounter;
    private int stacks;

    private void Start()
    {
        enemyCounter = FindObjectOfType<EnemyCounter>();
        
        // ReSharper disable once PossibleLossOfFraction
        PlayerStats.Instance.primaryDamage += Mathf.FloorToInt(enemyCounter.killedEnemies / 2);
    }

    public void OnKill(Component sender, object data)
    {
        stacks += 1;
        if (stacks < stacksForOneDamage) return;
        PlayerStats.Instance.primaryDamage += 1;
        stacks = 0;
    }
}
