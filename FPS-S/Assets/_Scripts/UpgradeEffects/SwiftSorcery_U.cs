using UnityEngine;

public class SwiftSorcery_U : MonoBehaviour
{
    [SerializeField] private float bonusMovementSpeedPercent;
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerMovement.slowWhileShooting *= (100 + bonusMovementSpeedPercent) / 100;
        playerMovement.fovWhileShooting = 64;
    }
}
