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
    [SerializeField] private float timeDutchIn,timeDutchOut;
    [SerializeField] private float dutch = 10f;

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
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
            virtualCamera.m_Lens.Dutch = Mathf.Lerp(virtualCamera.m_Lens.Dutch, -dutch, timeDutchIn);
        }
        else if (playerMovement.Straffe < 0)
        {
            virtualCamera.m_Lens.Dutch = Mathf.Lerp(virtualCamera.m_Lens.Dutch, dutch, timeDutchIn);
        }
        else
        {
            virtualCamera.m_Lens.Dutch = Mathf.Lerp(virtualCamera.m_Lens.Dutch, 0, timeDutchOut);
        }
    }

    private void UpdateCameraFOV()
    {
        float targetFOV = playerMovement.Translation != 0 || playerMovement.Straffe != 0 ? maxFOV : startFOV;
        virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, targetFOV, timeFOV);
    }
}

