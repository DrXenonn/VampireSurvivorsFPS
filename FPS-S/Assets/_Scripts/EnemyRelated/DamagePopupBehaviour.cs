using System.Collections;
using UnityEngine;

public class DamagePopupBehaviour : MonoBehaviour
{
    [SerializeField] private float timeToLive;
    private void OnEnable()
    {
        StartCoroutine(DeactivatePopupDelayed(gameObject, timeToLive));
    }

    private void OnDisable()
    {
        DamagePopupGenerator.instance.pool.Release(gameObject);
    }

    private static IEnumerator DeactivatePopupDelayed(GameObject popup, float delay)
    {
        yield return new WaitForSeconds(delay);
        popup.SetActive(false);
    }
}
