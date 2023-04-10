using UnityEngine;

public class Quickstep_U : MonoBehaviour
{
    private GameObject player;
    private PlayerMovement playerMovement;
    private Teleport teleport;
    [SerializeField] private float bonusMovementSpeedPercent;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        teleport = player.GetComponent<Teleport>();
        teleport.enabled = true;
        
        playerMovement.currentMovementSpeed *= (bonusMovementSpeedPercent + 100) / 100;
    }
}
