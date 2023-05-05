using UnityEngine;

public class Cactus : MonoBehaviour
{
    [SerializeField] private GameEvent dealDamage;
    [SerializeField] private int damage;
    [SerializeField] private float knockbackForce = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            dealDamage.Raise(damage);

            Rigidbody playerRigidbody = collision.collider.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                Vector3 direction = collision.contacts[0].point - transform.position;
                direction = direction.normalized;
                playerRigidbody.AddForce(direction * knockbackForce, ForceMode.Impulse);
            }
        }
    }
}
