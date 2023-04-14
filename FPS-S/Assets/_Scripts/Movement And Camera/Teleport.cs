using UnityEngine;

public class Teleport : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerCam;

    [Header("Teleport")]
    [SerializeField] private float tpDistance;

    [Header("Cooldown")]
    [SerializeField] private float tpCd;
    private float tpCdTimer;

    [Header("Input")]
    [SerializeField] private KeyCode tpKey = KeyCode.E;

    [Header("Bonus Movement Speed")]
    [SerializeField] private float bonusMovementSpeed;
    [SerializeField] private float bonusDuration;
    private float currentBonusDuration;

    private void Update()
    {
        if (Input.GetKeyDown(tpKey) && Time.time >= tpCdTimer)
        {
            TeleportFunc();

            // Apply bonus movement speed and reset duration
            currentBonusDuration = bonusDuration;
            PlayerStats.Instance.movementSpeed += bonusMovementSpeed;

            tpCdTimer = Time.time + tpCd;
        }

        // Decaying movement speed
        if (currentBonusDuration > 0)
        {
            PlayerStats.Instance.movementSpeed = Mathf.Lerp(PlayerStats.Instance.movementSpeed + bonusMovementSpeed,
                                                             PlayerStats.Instance.movementSpeed,
                                                             1 - (currentBonusDuration / bonusDuration));
            currentBonusDuration -= Time.deltaTime;
        }
    }

    private void TeleportFunc()
    {
        var verticalInput = Input.GetAxisRaw("Vertical");
        var horizontalInput = Input.GetAxisRaw("Horizontal");

        if (verticalInput == 0 && horizontalInput == 0)
        {
            verticalInput = 1;
            horizontalInput = 0;
        }

        transform.Translate(Vector3.right * horizontalInput * tpDistance);
        transform.Translate(Vector3.forward * verticalInput * tpDistance);
    }
}
