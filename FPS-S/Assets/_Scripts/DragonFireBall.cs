using System.Collections;
using UnityEngine;

public class DragonFireBall : MonoBehaviour
{
    [SerializeField] private float time;
    private Vector3 playerPos;
    private Vector3 initialPosition;
    private float timer;

    private void Start()
    {
        initialPosition = transform.position;
        playerPos = GameObject.FindWithTag("Player").transform.position;
        StartCoroutine(MoveToPlayer());
    }

    private IEnumerator MoveToPlayer()
    {
        while (timer < time)
        {
            timer += Time.deltaTime;
            var t = timer / time;
            transform.position = Vector3.Lerp(initialPosition, playerPos, t);
            yield return null;
        }
        
        Destroy(gameObject);
    }
}