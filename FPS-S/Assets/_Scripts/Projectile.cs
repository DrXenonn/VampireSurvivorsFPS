using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject boomVFX;
    [SerializeField] private GameObject particlesVFX;
    [SerializeField] private GameObject effects;

    [Header("Variables")]
    [SerializeField] private float timeToLive = 2.5f;
    [SerializeField] private float timeToEnable;
    private bool collided;


    private void Start()
    {
        Invoke("EnableObject", timeToEnable);
        Destroy(gameObject, timeToLive);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Projectile") && !collision.gameObject.CompareTag("Player") && !collided)
        {
            collided = true;
            Destroy(gameObject);

            ShowParticlesVfx(collision);

            if (collision.gameObject.CompareTag("Enemy"))
            {
                Hit(collision);
            }
        }
    }

    private void Hit(Collision collision)
    {
        ShowParticlesVfx(collision);
        ShowBoomVfx(collision);
    }

    private void EnableObject()
    {
        effects.SetActive(true);
    }

    private void ShowBoomVfx(Collision collision)
    {
        var impact = Instantiate(boomVFX, collision.contacts[0].point, Quaternion.identity);
        Destroy(impact, 2f);
    }

    private void ShowParticlesVfx(Collision collision)
    {
        var impact = Instantiate(particlesVFX, collision.contacts[0].point, Quaternion.identity);
        Destroy(impact, 2f);
    }

}
