using UnityEngine;

public class AdrenalineRush_U : MonoBehaviour
{
    [SerializeField] private int maxStacks = 20;
    [SerializeField] private float bonusMovementSpeedPercent = 5;
    [SerializeField] private float bonusFireRatePercent = 10;
    [SerializeField] private float bonusProjectileSpeedPercent = 10;
    [SerializeField] private GameEvent onBuffGain;

    private uint stacks = 0;
    private bool maxStacksReached = false;
    private float bonusMovementSpeed;
    private float bonusProjectileSpeed;
    private float bonusFireRate;

    private void UpdateStacks()
    {
        stacks++;
    }

    private void ResetStacks()
    {
        stacks = 0;
    }

    public void OnTakeDamage()
    {
        PlayerStats.Instance.movementSpeed -= bonusMovementSpeed;
        bonusMovementSpeed = 0;
        ResetStacks();
    }

    public void OnEnemyKilled()
    {
        UpdateStacks();

        if (stacks < maxStacks)
        {
            bonusMovementSpeed += PlayerStats.Instance.GainBonus("movementSpeed", bonusMovementSpeedPercent);
        }
    }

    private void ApplyBuff()
    {
        bonusFireRate = PlayerStats.Instance.GainBonus("fireRate", bonusFireRatePercent);
        bonusProjectileSpeed = PlayerStats.Instance.GainBonus("projectileSpeed", bonusProjectileSpeedPercent);
    }

    private void RemoveBuff()
    {
        PlayerStats.Instance.fireRate -= bonusFireRate;
        PlayerStats.Instance.projectileSpeed -= bonusProjectileSpeed;
    }

    private void Update()
    {
        if (stacks >= maxStacks && !maxStacksReached)
        {
            onBuffGain.Raise();
            ApplyBuff();
            maxStacksReached = true;
        }
        else if (stacks < maxStacks && maxStacksReached)
        {
            RemoveBuff();
            maxStacksReached = false;
        }
    }
}
