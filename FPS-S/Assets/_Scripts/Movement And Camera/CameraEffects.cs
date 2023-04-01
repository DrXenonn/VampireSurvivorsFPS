using Cinemachine;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private PlayerMovement playerMovement;

    [Header("Variables")]
    [SerializeField] private float startFOV = 60f;
    [SerializeField] private float maxFOV = 70f;
    [SerializeField] private float timeFOV = 0.2f;
    [SerializeField] private float timeDutch = 0.2f;
    [SerializeField] private float dutch = 10f;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        UpdateCamera();
    }


    private void UpdateCamera()
    {
        UpdateCameraDutch();
        UpdateCameraFOV();
    }

    private void UpdateCameraDutch()
    {
        if (playerMovement.Straffe > 0)
        {
            virtualCamera.m_Lens.Dutch = Mathf.Lerp(virtualCamera.m_Lens.Dutch, -dutch, timeDutch);
        }
        else if (playerMovement.Straffe < 0)
        {
            virtualCamera.m_Lens.Dutch = Mathf.Lerp(virtualCamera.m_Lens.Dutch, dutch, timeDutch);
        }
        else
        {
            virtualCamera.m_Lens.Dutch = Mathf.Lerp(virtualCamera.m_Lens.Dutch, 0, timeDutch);
        }
    }

    private void UpdateCameraFOV()
    {
        float targetFOV = playerMovement.Translation != 0 || playerMovement.Straffe != 0 ? maxFOV : startFOV;
        virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, targetFOV, timeFOV);
    }
}

