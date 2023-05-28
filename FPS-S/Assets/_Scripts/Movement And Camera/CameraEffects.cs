using Cinemachine;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    [Header("References")]
    private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private PlayerMovement playerMovement;

    [Header("Variables")]
    [SerializeField] private float startFOV = 60f;
    public float maxFOV = 70f;
    [SerializeField] private float timeFOV = 0.2f;
    [SerializeField] private float dutchTime;
    [SerializeField] private float dutch = 10f;
    [SerializeField] private float walkBobSpeed = 14f;
    [SerializeField] private float walkBobAmount = 0.5f;
    private float defaulYPos = 0f;
    private float timer;

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        defaulYPos = virtualCamera.transform.localPosition.y;
    }

    private void Update()
    {
        CheckIfPlayerIsMoving();
        UpdateCamera();
    }

    public void UpdateCamera()
    {
        UpdateCameraDutch();
        UpdateCameraFOV();
        HandleHeadBob();
    }

    private void UpdateCameraDutch()
    {
        float targetDutch = 0f;

        if (playerMovement.horizontalMovement > 0)
        {
            targetDutch = -dutch;
        }
        else if (playerMovement.horizontalMovement < 0)
        {
            targetDutch = dutch;
        }

        virtualCamera.m_Lens.Dutch = Mathf.Lerp(virtualCamera.m_Lens.Dutch, targetDutch, dutchTime);
    }

     private void UpdateCameraFOV()
    {
        float targetFOV = CheckIfPlayerIsMoving() ? maxFOV : startFOV;
        virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, targetFOV, timeFOV);
    }

    private void HandleHeadBob()
    {
        if (!playerMovement.isShooting && CheckIfPlayerIsMoving()) 
        {
            timer += Time.deltaTime * walkBobSpeed;
            virtualCamera.transform.localPosition = new Vector3(
                virtualCamera.transform.localPosition.x,
                defaulYPos + Mathf.Sin(timer) * walkBobAmount,
                virtualCamera.transform.localPosition.z);
        }
    }

    private bool CheckIfPlayerIsMoving()
    {
        return playerMovement.verticalMovement != 0 || playerMovement.horizontalMovement != 0;
    }
}

