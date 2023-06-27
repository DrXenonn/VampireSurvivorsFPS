using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BerserkMode : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float berserkModeTime;
    [SerializeField] private RawImage icon;
    private float normalSpeed;

    private void Start()
    {
        normalSpeed = agent.speed;
    }

    private void OnEnable()
    {
        CancelInvoke(nameof(GoBerserk));
        Invoke(nameof(GoBerserk), berserkModeTime);
    }

    private void GoBerserk()
    {
        agent.speed = PlayerStats.Instance.movementSpeed + 3f;
        icon.enabled = true;
    }

    public void BecomeNormal() 
    {
        agent.speed = normalSpeed;
        icon.enabled = false;
        Invoke(nameof(GoBerserk), berserkModeTime);
    }
}