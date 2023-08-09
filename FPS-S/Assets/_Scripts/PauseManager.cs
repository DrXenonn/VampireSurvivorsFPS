using System;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private MouseLook mouseLook;
    [SerializeField] private Shooting shooting;
    [SerializeField] private GameEvent onPause;
    [SerializeField] private GameEvent onResume;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject upgradePanel;
    private bool isPaused;

    private void OnEnable()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !upgradePanel.activeSelf)
        {
            if (!isPaused)
            {
                PauseGame();
                ShowPauseMenu();
            }
            else
            {
                UnPauseGame();
                HidePauseMenu();
            }
        }
    }

    public void PauseGame(bool stopTime = true)
    {
        if (stopTime)
        {
            Time.timeScale = 0;
        }
        Cursor.lockState = CursorLockMode.None;
        mouseLook.enabled = false;
        shooting.enabled = false;
        onPause.Raise();
        isPaused = true;
    }

    public void UnPauseGame()
    {
        if (EndGame.Instance.isGameEnded) return;

        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        mouseLook.enabled = true;
        shooting.enabled = true;
        onResume.Raise();
        isPaused = false;
    }

    private void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
    }

    private void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
    }
    
    public void OnResume()
    {
        UnPauseGame();
        pauseMenu.SetActive(false);
    }

    public void OnGiveUp()
    {
       SceneManager.Instance.LoadScene("Main Menu");
       //SceneManager.Instance.UnloadScene("Game");
    }

    public void OnOpenSettings()
    {
        settingsMenu.SetActive(true);
    }
}