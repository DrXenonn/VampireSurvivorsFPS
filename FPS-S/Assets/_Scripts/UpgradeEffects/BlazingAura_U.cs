using System.Collections;
using UnityEngine;

public class BlazingAura_U : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private int tickDamage;
    [SerializeField] private float tickRate;
    [SerializeField] private GameObject fireParticles;
    [SerializeField] private ParticleSystem burnAura;
    [SerializeField] private AudioSource source;
    private GameObject player;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating(nameof(BurnEnemiesInside), 0, tickRate);
    }

    private void BurnEnemiesInside()
    {
        transform.position = player.transform.position;
        
        foreach (Collider hitCollider in GetEnemiesInsideTheRadius())
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                var enemyHp = hitCollider.GetComponent<EnemyHP>();
                var enemyPos = enemyHp.gameObject.transform.position;
                enemyPos = new Vector3(enemyPos.x, enemyPos.y + 0.55f, enemyPos.z);
                var fire = Instantiate(fireParticles, enemyPos, Quaternion.identity, hitCollider.transform);
                Destroy(fire, 1);
                
                StartCoroutine(nameof(ShowBurnAura));
                PlaySound();
                enemyHp.TakeDamage(Mathf.FloorToInt(tickDamage * PlayerStats.Instance.elementalDamageMultiplier), "Burn");
            }
        }
    }
    

    private Collider[] GetEnemiesInsideTheRadius()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        return hitColliders;
    }

    private IEnumerator ShowBurnAura()
    {
        burnAura.Play();
        yield return new WaitForSeconds(0f);
        burnAura.Stop();
    }

    private void PlaySound()
    {
        source.Play();
    }
}