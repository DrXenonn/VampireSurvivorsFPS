using System;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private MouseLook mouseLook;
    [SerializeField] private Shooting shooting;
    [SerializeField] private GameEvent onPause;
    [SerializeField] private GameEvent onResume;
    private bool isPaused;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                UnPauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        mouseLook.enabled = false;
        shooting.enabled = false;
        onPause.Raise();
        isPaused = true;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        mouseLook.enabled = true;
        shooting.enabled = true;
        onResume.Raise();
        isPaused = false;
    }

    private void ShowPauseMenu()
    {
        
    }
}