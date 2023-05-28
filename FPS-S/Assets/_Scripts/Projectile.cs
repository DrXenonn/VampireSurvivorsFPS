using UnityEngine;
public class Projectile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ParticleSystem particlesVFX;
    [SerializeField] private Rigidbody rb;


    [Header("Variables")]
    [SerializeField] private int randomness;
    private bool collided;

    private void OnEnable()
    {
        particlesVFX.Stop();
        Shoot();
        collided = false;
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
        int calculatedDamage = CalculateDamage(PlayerStats.Instance.primaryDamage, randomness);
        collision.gameObject.GetComponent<EnemyHP>().TakeDamage(calculatedDamage);
    }

    int CalculateDamage(int damage, int random)
    {
        return Random.Range(damage - Mathf.RoundToInt(random / 2f), damage + random);
    }

    private void Shoot() 
    {
        Vector3 target = Crosshair.Instance.destination;
        Vector3 startPos = Shooting.Instance.firePos.position;
        float speed = PlayerStats.Instance.projectileSpeed;
        rb.velocity = (target - startPos).normalized * speed;
    }
}
