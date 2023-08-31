using System.Collections;
using TMPro;
using UnityEngine;

public class OnSpiderSpawn : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private float activationTime, showTime;
    private void Start()
    {
        tmp.enabled = false;
        StartCoroutine(nameof(PlayAnimation));
    }

    private IEnumerator PlayAnimation()
    {
        yield return new WaitForSeconds(activationTime);
        animator.enabled = true;
        tmp.enabled = true;
        yield return new WaitForSeconds(showTime);
        gameObject.SetActive(false);
    }
}
