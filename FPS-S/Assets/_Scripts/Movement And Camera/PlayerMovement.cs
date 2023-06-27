using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CameraEffects cameraEffects;
    [SerializeField] private float fovDelay = 0.2f;
    private int fovWhileRunning = 65;
    private float shootSlowdownFactor = 1f;
    private float timer;

    public float slowWhileShooting = 0.5f;
    [HideInInspector] public int fovWhileShooting = 60;
    [HideInInspector] public float verticalMovement;
    [HideInInspector] public float horizontalMovement;
    [HideInInspector] public bool isShooting;

    private void Awake()
    {
        fovWhileRunning = (int)cameraEffects.maxFOV;
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

        horizontalMovement = inputVector.x * PlayerStats.Instance.movementSpeed * shootSlowdownFactor * Time.deltaTime;
        verticalMovement = inputVector.z * PlayerStats.Instance.movementSpeed * shootSlowdownFactor * Time.deltaTime;
        transform.Translate(horizontalMovement, 0, verticalMovement);
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
