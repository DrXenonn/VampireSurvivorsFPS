using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CameraEffects cameraEffects;
    [SerializeField] private float slowWhileShooting = 0.5f;
    [SerializeField] private float fovDelay = 0.2f;
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
        translation = Input.GetAxis("Vertical") * PlayerStats.Instance.movementSpeed * shootSlowdownFactor * Time.deltaTime;
        straffe = Input.GetAxis("Horizontal") * PlayerStats.Instance.movementSpeed * shootSlowdownFactor * Time.deltaTime;
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
            cameraEffects.maxFOV = 60f;
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
        cameraEffects.maxFOV = 67f;
    }
}
