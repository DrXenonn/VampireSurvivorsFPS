using System.Collections;
using UnityEngine;

public class Web : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float lifeTime;
    [SerializeField] private float stunDuration;

    private GameObject player;
    private Rigidbody rb;
    private Vector3 target;
    private GameObject stunEffect;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        stunEffect = GameObject.FindWithTag("Web");
        var playerPos = player.transform.position;
        target = new Vector3(playerPos.x, 1.5f, playerPos.z);
        rb = GetComponent<Rigidbody>();
        MoveToPlayer();
        Destroy(gameObject, lifeTime);
    }

    private void MoveToPlayer()
    {
        var direction = (target - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(nameof(StunPlayer));
        }
    }

    private IEnumerator StunPlayer()
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        stunEffect.transform.GetChild(0).gameObject.SetActive(true);
        player.GetComponent<PlayerMovement>().enabled = false;
        yield return new WaitForSeconds(stunDuration);
        player.GetComponent<PlayerMovement>().enabled = true;
        stunEffect.transform.GetChild(0).gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        player.GetComponent<PlayerMovement>().enabled = true;
        stunEffect.transform.GetChild(0).gameObject.SetActive(false);
    }
}