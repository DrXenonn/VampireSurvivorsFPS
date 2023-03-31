using UnityEngine;

public class Teleport : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private Transform playerCam;

    [Header("Teleport")]
    [SerializeField] private float tpDistance;

    [Header("Cooldown")]
    [SerializeField] private float tpCd;
    private float tpCdTimer;

    [Header("Input")]
    [SerializeField] private KeyCode tpKey = KeyCode.E;

    private void Update()
    {
        if (Input.GetKeyDown(tpKey) && Time.time >= tpCdTimer)
        {
            TeleportFunc();
            tpCdTimer = Time.time + tpCd;
        }
    }

    void TeleportFunc()
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

