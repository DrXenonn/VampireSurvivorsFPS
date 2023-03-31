using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [Header("Variables")]
    [SerializeField] private float startFOV, maxFOV;
    [SerializeField] private float timeFOV;
    [SerializeField] private float timeDutch;
    [SerializeField] private float dutch;
    [HideInInspector] public float translation;
    [HideInInspector] public float straffe;
    [HideInInspector] public float currentMovementSpeed;

    private void Start()
    {
        currentMovementSpeed = playerStats.movementSpeed;
    }

    private void Update()
    {
        translation = Input.GetAxis("Vertical") * currentMovementSpeed * Time.deltaTime;
        straffe = Input.GetAxis("Horizontal") * currentMovementSpeed * Time.deltaTime;
        transform.Translate(straffe, 0, translation);

        if (straffe > 0)
        {
            virtualCamera.m_Lens.Dutch = Mathf.Lerp(virtualCamera.m_Lens.Dutch, -dutch, timeDutch);
        }
        else if (straffe < 0)
        {
            virtualCamera.m_Lens.Dutch = Mathf.Lerp(virtualCamera.m_Lens.Dutch, dutch, timeDutch);
        }
        else
        {
            virtualCamera.m_Lens.Dutch = Mathf.Lerp(virtualCamera.m_Lens.Dutch, 0, timeDutch);
        }

        if (translation != 0 || straffe != 0)
        {
            virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, maxFOV, timeFOV);
        }
        else
        {
            virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, startFOV, timeFOV);
        }
    }
}
