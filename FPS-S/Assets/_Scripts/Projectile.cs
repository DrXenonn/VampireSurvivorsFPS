using UnityEngine;
public class Projectile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ParticleSystem particlesVFX;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private TrailRenderer trailRenderer;


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
        int calculatedDamage = CalculateDamage(PlayerStats.Instance.primaryDamage, randomness, out var isCritical);
        collision.gameObject.GetComponent<EnemyHP>().TakeDamage(calculatedDamage, isCritical);
    }

    int CalculateDamage(int damage, int random, out bool isCritical)
    {
        int step = 5;
        int minDamage = Mathf.FloorToInt((damage - random) / (float)step) * step;
        int maxDamage = Mathf.CeilToInt((damage + random) / (float)step) * step;

        int randomSteps = Mathf.RoundToInt((maxDamage - minDamage) / (float)step);
        int randomIndex = Random.Range(0, randomSteps + 1);

        int calculatedDamage = minDamage + randomIndex * step;

        isCritical = false;
        if (Random.value < PlayerStats.Instance.criticalChance)
        {
            calculatedDamage *= PlayerStats.Instance.criticalMultiplier;
            isCritical = true;
        }
        
        return calculatedDamage;
    }

    
    private void Shoot() 
    {
        Vector3 target = Crosshair.Instance.destination;
        Vector3 startPos = Shooting.Instance.firePos.position;
        float speed = PlayerStats.Instance.projectileSpeed;
        rb.velocity = (target - startPos).normalized * speed;
    }
}
