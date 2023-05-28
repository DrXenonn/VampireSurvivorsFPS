using UnityEngine;

public class Cactus : MonoBehaviour
{
    [SerializeField] private GameEvent dealDamage;
    [SerializeField] private int damage;
    [SerializeField] private float knockbackForce = 10f;

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            dealDamage.Raise(damage);

            Rigidbody playerRigidbody = other.collider.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                Vector3 direction = other.contacts[0].point - transform.position;
                direction = direction.normalized;
                playerRigidbody.AddForce(direction * knockbackForce, ForceMode.Impulse);
            }
        }
    }
}
