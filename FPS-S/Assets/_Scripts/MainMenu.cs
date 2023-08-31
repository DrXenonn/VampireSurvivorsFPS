using System;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject playMenu, settingsMenu, augmentsMenu;

    private void OnEnable()
    {
        Time.timeScale = 1;
    }

    public void OnPlay()
    {
        playMenu.SetActive(true);
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