using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float slowWhileShooting = 0.5f;
    [HideInInspector] public int fovWhileShooting = 60;
    [SerializeField] private CameraEffects cameraEffects;
    [SerializeField] private float fovDelay = 0.2f;
    private int fovWhileRunning = 67;
    private float translation;
    private float straffe;
    private float shootSlowdownFactor = 1f;
    private bool isShooting;
    private float timer;
    public float Translation
    {
        get { return translation; }
    }

    public float Straffe
    {
        get { return straffe; }
    }

    private void Update()
    {
        MovePlayer();
        CheckIfShooting();
        CheckFovDelay();
    }

    private void MovePlayer()
    {
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        inputVector = Vector3.ClampMagnitude(inputVector, 1f);

        translation = inputVector.z * PlayerStats.Instance.movementSpeed * shootSlowdownFactor * Time.deltaTime;
        straffe = inputVector.x * PlayerStats.Instance.movementSpeed * shootSlowdownFactor * Time.deltaTime;
        transform.Translate(straffe, 0, translation);
    }


    private void CheckIfShooting()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            SlowDownPlayer();
            isShooting = true;
            timer = 0f;
        }
        else if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            isShooting = false;
            SetToNormalSpeed();
        }
    }

    private void CheckFovDelay()
    {
        if (isShooting && timer >= fovDelay)
        {
            cameraEffects.maxFOV = fovWhileShooting;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    private void SlowDownPlayer()
    {
        shootSlowdownFactor = slowWhileShooting;
    }

    private void SetToNormalSpeed()
    {
        shootSlowdownFactor = 1f;
        cameraEffects.maxFOV = fovWhileRunning;
    }
}
