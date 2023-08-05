using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BerserkMode : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float berserkModeTime, warningTime;
    [SerializeField] private RawImage icon;
    [SerializeField] private Texture closeToBerserkTexture, berserkTexture;
    private float normalSpeed;

    private void Start()
    {
        normalSpeed = agent.speed;
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(nameof(GoBerserk));
    }

    public void BecomeNormal() 
    {
        agent.speed = normalSpeed;
        icon.enabled = false;
        StopAllCoroutines();
        StartCoroutine(nameof(GoBerserk));
    }

    private IEnumerator GoBerserk()
    {
        icon.enabled = false;
        yield return new WaitForSeconds(berserkModeTime);

        icon.texture = closeToBerserkTexture;
        icon.enabled = true;

        yield return new WaitForSeconds(warningTime);

        icon.texture = berserkTexture;
        icon.enabled = true;
        agent.speed = PlayerStats.Instance.movementSpeed + 5f;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}