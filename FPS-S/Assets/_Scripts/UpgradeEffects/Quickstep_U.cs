using UnityEngine;

public class Quickstep_U : MonoBehaviour
{
    [SerializeField] private float bonusMovementSpeedPercent;

    void Start()
    {
        PlayerStats.Instance.GainBonus("movementSpeed",bonusMovementSpeedPercent);
    }
}
