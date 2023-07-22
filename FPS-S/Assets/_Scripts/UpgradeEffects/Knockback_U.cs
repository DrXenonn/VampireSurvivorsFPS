using System;
using UnityEngine;

public class Knockback_U : MonoBehaviour
{
    [SerializeField] private float pushForce;
    [SerializeField] private float pushDuration;
    [SerializeField] private int bonusBaseDamagePercent;

    private void Start()
    {
        PlayerStats.Instance.GainBaseDamage(bonusBaseDamagePercent);
    }

    public void Knockback(Component sender, object data)
    {
        var objectToPushAway = (GameObject)data;
        if (!objectToPushAway.GetComponent<OnEnemyExitMap>().isEnemyInside) return;

        var direction = objectToPushAway.transform.position - Camera.main!.transform.position;
        direction.y = 0;
        
        var pushVector = direction.normalized * pushForce;
        
        StartCoroutine(KnockbackCoroutine(objectToPushAway, pushVector));
    }

    private System.Collections.IEnumerator KnockbackCoroutine(GameObject target, Vector3 pushVector)
    {
        var elapsed = 0f;
        var initialPosition = target.transform.position;

        while (elapsed < pushDuration)
        {
            var newPos = Vector3.Lerp(initialPosition, initialPosition + pushVector, elapsed / pushDuration);
            target.transform.position = newPos;

            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}