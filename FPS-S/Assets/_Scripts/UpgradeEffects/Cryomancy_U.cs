using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Cryomancy_U : MonoBehaviour
{
    [SerializeField] private float stunChance;
    [SerializeField] private float stunTime;
    [SerializeField] private int frostDamage;
    [SerializeField] private GameObject stunEffect;
    [SerializeField] private Mesh iceWand;
    [SerializeField] private AudioSource shatter;
    [ColorUsage(true,true)]
    [SerializeField] private Color color, emissionColor;
    [HideInInspector] public bool hasFrozenImpact;
    private float originalStunChance;
    private GameObject stunEffectInstance;

    private void Start()
    {
        var wand = GameObject.FindGameObjectWithTag("Wand").GetComponent<MeshFilter>();
        wand.mesh = iceWand;
        MaterialColorController.instance.SetNewColors(color, emissionColor);
        originalStunChance = stunChance;
    }

    public void StunOnHit(Component sender, object data)
    {
        var objectToStun = (GameObject)data;
        var enemyNavmesh = objectToStun.GetComponent<NavMeshAgent>();

        DealFrostDamage(objectToStun);

        if ((Random.value < stunChance))
        {
            if (enemyNavmesh != null && enemyNavmesh.isOnNavMesh)
            {
                StartCoroutine(Stun(enemyNavmesh));
            }
        }

        stunChance = originalStunChance;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator Stun(NavMeshAgent enemyNavMesh)
    {
        PlayStunEffect(enemyNavMesh);

        BecomeStunned(enemyNavMesh);
        yield return new WaitForSeconds(stunTime);
        UnStun(enemyNavMesh);
    }

    private void BecomeStunned(NavMeshAgent enemyNavMesh)
    {
        ChangeStunState(enemyNavMesh, true);
        ChangeAnimationSpeed(enemyNavMesh.gameObject, 0.25f);
        ChangeAudioState(enemyNavMesh.gameObject, false);
        ChangeDeBuffState(enemyNavMesh.gameObject, true);
    }

    private void UnStun(NavMeshAgent enemyNavMesh)
    {
        ChangeStunState(enemyNavMesh, false);
        ChangeAnimationSpeed(enemyNavMesh.gameObject, 1);
        ChangeAudioState(enemyNavMesh.gameObject, true);
        ChangeDeBuffState(enemyNavMesh.gameObject, false);
    }

    private void ChangeStunState(NavMeshAgent enemyNavMesh, bool isStopped)
    {
        if (enemyNavMesh != null && enemyNavMesh.isOnNavMesh && enemyNavMesh.isActiveAndEnabled)
        {
            enemyNavMesh.isStopped = isStopped;
        }
    }

    private void ChangeAnimationSpeed(GameObject enemy, float animationSpeed)
    {
        enemy.GetComponent<Animator>().speed = animationSpeed;
    }

    private void ChangeAudioState(GameObject enemy, bool shouldAudioPlay)
    {
        var audioSource = enemy.GetComponent<AudioSource>();
        if (audioSource == null) return;

        audioSource.volume = shouldAudioPlay ? 1 : 0;
    }


    private void PlayStunEffect(NavMeshAgent enemyNavMesh)
    {
        stunEffectInstance = Instantiate(stunEffect, GetEnemyPos(enemyNavMesh), Quaternion.identity, enemyNavMesh.transform);
        Destroy(stunEffectInstance, stunTime);
    }

    private Vector3 GetEnemyPos(NavMeshAgent enemyNavMesh)
    {
        var enemyTransform = enemyNavMesh.gameObject.transform;
        var enemyPos = enemyTransform.position;
        return new Vector3(enemyPos.x, enemyPos.y + 0.55f, enemyPos.z);
    }

    private void ChangeDeBuffState(GameObject enemy, bool isDeBuffApplied)
    {
        var enemyHp = enemy.GetComponent<EnemyHP>();
        enemyHp.isDeBuffApplied = isDeBuffApplied;
    }

    private void DealFrostDamage(GameObject enemy)
    {
        if (!hasFrozenImpact) return;

        var enemyHp = enemy.GetComponent<EnemyHP>();

        if (enemyHp.isDeBuffApplied)
        {
            enemyHp.TakeDamage(Mathf.FloorToInt(frostDamage * PlayerStats.Instance.elementalDamageMultiplier), "Frost");
            UnStun(enemy.GetComponent<NavMeshAgent>());
            ChangeDeBuffState(enemy, false);
            Destroy(stunEffectInstance);
            shatter.Play();
            stunChance = 0;
        }
    }
}