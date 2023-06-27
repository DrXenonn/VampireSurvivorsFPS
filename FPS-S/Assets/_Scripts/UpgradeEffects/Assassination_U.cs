using UnityEngine;

public class Assassination_U : MonoBehaviour
{
    [SerializeField] private float executionThreshold;

    private void Awake()
    {
        PlayerStats.Instance.executionThreshold = executionThreshold;
    }
}