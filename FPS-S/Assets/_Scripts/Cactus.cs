using UnityEngine;

public class Cactus : MonoBehaviour
{
    [SerializeField] private GameEvent dealDamage;
    [SerializeField] private int damage;
    [SerializeField] private float knockBackForce = 75f;

    private void OnCollisionEnter(Collision other)
    {
        if (!other.collider.CompareTag("Player")) return;
        dealDamage.Raise(damage);

        var playerRigidbody = other.collider.GetComponent<Rigidbody>();

        if (playerRigidbody == null) return;
        var direction = other.contacts[0].point - transform.position;
        direction = direction.normalized;
        playerRigidbody.AddForce(direction * knockBackForce, ForceMode.Impulse);
    }
}