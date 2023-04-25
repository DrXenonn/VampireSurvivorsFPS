using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ParticleSystem boomVFX;
    [SerializeField] private ParticleSystem particlesVFX;
    [SerializeField] private GameObject damagePopupPrefab;
    [SerializeField] private Rigidbody rb;


    [Header("Variables")]
    [SerializeField] private int randomness;
    private bool collided;


    private void OnEnable()
    {
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
                ShowParticlesVfx(collision);
                ShowBoomVfx(collision);
                Damage(collision);
            }
            gameObject.SetActive(false);
        }
    }

    private void ShowBoomVfx(Collision collision)
    {
        boomVFX.transform.parent = null;
        boomVFX.transform.position = gameObject.transform.position;
        boomVFX.Play();
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
        collision.gameObject.GetComponent<EnemyHP>().currentEnemyHP -= calculatedDamage;
        ShowDamagePopup(collision, calculatedDamage);
    }

    int CalculateDamage(int damage, int random)
    {
        return Random.Range(damage - Mathf.RoundToInt(random / 2f), damage + random);
    }

    private void ShowDamagePopup(Collision collision, int damage)
    {
        var text = Instantiate(damagePopupPrefab, transform.position, Quaternion.identity, collision.transform);
        text.GetComponentInChildren<TextMesh>().text = damage.ToString();
    }

    private void Shoot() 
    {
        Vector3 target = Crosshair.Instance.destination;
        Vector3 startPos = Shooting.Instance.firePos.position;
        float speed = PlayerStats.Instance.projectileSpeed;

        rb.velocity = (target - startPos).normalized * speed;
    }
}
