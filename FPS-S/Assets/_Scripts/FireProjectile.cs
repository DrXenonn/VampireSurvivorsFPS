using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject damagePopupPrefab;

    [Header("Variables")]
    [SerializeField] private int randomness;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Damage(collision);
        }

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
}
