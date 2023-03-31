using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject boomVFX;
    [SerializeField] private GameObject particlesVFX;
    [SerializeField] private GameObject effects;
    [SerializeField] private GameObject damagePopupPrefab;
    private ClassHandler classHandler;

    [Header("Variables")]
    [SerializeField] private float timeToLive = 2.5f;
    [SerializeField] private float timeToEnable;
    [SerializeField] private int randomness;
    private int currentAttackDamage;
    private bool collided;

    private void Awake()
    {
        classHandler = FindObjectOfType<ClassHandler>();
    }

    private void Start()
    {
        Invoke("EnableObject", timeToEnable);
        Destroy(gameObject, timeToLive);
        currentAttackDamage = classHandler.chosenClass.startingPrimaryAD;
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
        int calculatedDamage = CalculateDamage(currentAttackDamage, randomness);
        collision.gameObject.GetComponent<EnemyHP>().currentEnemyHP -= calculatedDamage;
        ShowDamagePopup(collision, calculatedDamage);
        ShowParticlesVfx(collision);
        ShowBoomVfx(collision);
    }

    private void EnableObject()
    {
        effects.SetActive(true);
    }

    int CalculateDamage(int damage, int random)
    {
        return Random.Range(damage - Mathf.RoundToInt(random / 2f), damage + random);
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

    private void ShowDamagePopup(Collision collision, int damage)
    {
        var text = Instantiate(damagePopupPrefab, transform.position, Quaternion.identity, collision.transform);
        text.GetComponentInChildren<TextMesh>().text = damage.ToString();
    }

}
