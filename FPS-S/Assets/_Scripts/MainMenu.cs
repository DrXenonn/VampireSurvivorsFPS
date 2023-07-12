using System;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject augmentsMenu;

    private void OnEnable()
    {
        Time.timeScale = 1;
    }

    public void OnPlay()
    {
        SceneManager.Instance.LoadScene("Game");
        //SceneManager.Instance.UnloadScene("Main Menu");
    }

    public void OnSettingsOpen()
    {
        settingsMenu.SetActive(true);
        gameObject.SetActive(false);
    }
    
    public void OnAugmentsOpen()
    {
        augmentsMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnExit()
    {
        Application.Quit();
    }
    
}
