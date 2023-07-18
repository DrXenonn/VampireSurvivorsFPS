using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ParticleSystem particlesVFX;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private GameEvent onProjectileHitEnemy;


    [Header("Variables")]
    [SerializeField] private int randomness;
    private bool collided;

    private void OnEnable()
    {
        particlesVFX.Stop();
        Shoot();
        collided = false;

        trailRenderer.Clear();
        trailRenderer.enabled = false;
        trailRenderer.enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Projectile") && !collision.gameObject.CompareTag("Player") && !collided)
        {
            collided = true;  
            ShowParticlesVfx(collision);

            if (collision.gameObject.CompareTag("Enemy"))
            {
                Damage(collision);
                onProjectileHitEnemy.Raise(collision.gameObject);
            }
            gameObject.SetActive(false);
        }
    }

    private void ShowParticlesVfx(Collision collision)
    {
        particlesVFX.transform.parent = null;
        particlesVFX.transform.position = gameObject.transform.position;
        particlesVFX.Play();
    }

    private void Damage(Collision collision)
    {
        int calculatedDamage = CalculateDamage(PlayerStats.Instance.primaryDamage, randomness, out var dmgType);
        collision.gameObject.GetComponent<EnemyHP>().TakeDamage(calculatedDamage, dmgType);
    }

    private int CalculateDamage(int damage, int random, out string dmgType)
    {
        const int step = 5;
        var minDamage = Mathf.FloorToInt((damage - random) / (float)step) * step;
        var maxDamage = Mathf.CeilToInt((damage + random) / (float)step) * step;

        var randomSteps = Mathf.RoundToInt((maxDamage - minDamage) / (float)step);
        var randomIndex = Random.Range(0, randomSteps + 1);

        float calculatedDamage = minDamage + randomIndex * step;

        dmgType = "";
        if (Random.value < PlayerStats.Instance.criticalChance)
        {
            calculatedDamage *= PlayerStats.Instance.criticalMultiplier;

            dmgType = "Critical";
        }
        
        return (int)calculatedDamage;
    }

    
    private void Shoot() 
    {
        var target = Crosshair.Instance.destination;
        var startPos = Shooting.Instance.firePos.position;
        var speed = PlayerStats.Instance.projectileSpeed;
        rb.velocity = (target - startPos).normalized * speed;
    }
}
