using System;
using System.Collections;
using UnityEngine;

public class DragonBehaviour : MonoBehaviour
{
    [SerializeField] private float meleeInterval, rangeInterval;
    [SerializeField] private float distanceThreshold;
    [SerializeField] private float meleeAttackDelay;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject meleeDamageArea;
    [SerializeField] private GameObject rangeDamageArea;
    [SerializeField] private GameObject rangeProjectile;
    [SerializeField] private Transform firePos;
    [SerializeField] private BossHp bossHp;
    private Transform playerTransform;
    private int phase;

    private void Start()
    {
        phase = 1;
        playerTransform = GameObject.FindWithTag("Player").transform;
        InvokeRepeating(nameof(RangeAttack), rangeInterval, rangeInterval);
        InvokeRepeating(nameof(MeleeAttack), meleeInterval, meleeInterval);
        InvokeRepeating(nameof(CheckChangePhase), 1, 2);
    }

    private bool IsCloseToPlayer()
    {
        return Vector3.Distance(transform.position, playerTransform.position) < distanceThreshold;
    }

    private void MeleeAttack()
    {
        if (!IsCloseToPlayer()) return;

        animator.Play("DragonArmature|Dragon_Attack");
        StartCoroutine(ShowDamageArea(meleeDamageArea, meleeAttackDelay));
    }

    private void RangeAttack()
    {
        if (IsCloseToPlayer()) return;

        animator.Play("DragonArmature|Dragon_Attack2");
        StartCoroutine(AttackLate());
    }

    private void ChangePhase()
    {
        rangeInterval /= 1.5f;
        meleeInterval = 1.25f;
        CancelInvoke(nameof(RangeAttack));
        CancelInvoke(nameof(MeleeAttack));
        InvokeRepeating(nameof(RangeAttack), 0.5f, rangeInterval);
        InvokeRepeating(nameof(MeleeAttack), 0.5f, meleeInterval);
    }

    private void CheckChangePhase()
    {
        if (bossHp.GetHpAsPercent() < 50 && phase == 1)
        {
            ChangePhase();
            phase = 2;
        }
        else if (bossHp.GetHpAsPercent() < 25 && phase == 2)
        {
            ChangePhase();
            phase = 3;
        }
    }


    private IEnumerator AttackLate()
    {
        yield return new WaitForSeconds(0.4f);
        transform.LookAt(playerTransform);
        Instantiate(rangeDamageArea, playerTransform.position, Quaternion.Euler(90, 0, 0));
        Instantiate(rangeProjectile, firePos.position, Quaternion.identity);
    }

    private IEnumerator ShowDamageArea(GameObject obj, float delay)
    {
        obj.SetActive(true);
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
    }
}