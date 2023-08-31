using System;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private AudioMixer musicMixer, soundEffectsMixer;
    [SerializeField] private Slider musicSlider, soundEffectsSlider;
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle fullscreenToggle, vsyncToggle;
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private Toggle cameraShake;
    [SerializeField] private bool shouldDisableOnStart;
    
    private float musicVolume;
    private float soundEffectsVolume;
    private float sensitivity;
    private bool isFullscreen;
    private bool isVSyncOn;
    private int qualityLevel;
    private bool isCameraShakeEnabled;

    private void Start()
    {
        LoadSettings();
        UpdateUI();
        
        if (shouldDisableOnStart)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        LoadSettings();
        UpdateUI();
    }

    public void SetMusicVolume(float volume)
    {
        volume = (volume < -28f) ? -80f : volume;
        musicMixer.SetFloat("Music", volume);
        musicVolume = volume;
    }

    public void SetSoundEffectVolume(float volume)
    {
        volume = (volume < -28f) ? -80f : volume;
        soundEffectsMixer.SetFloat("SoundEffects", volume);
        soundEffectsVolume = volume;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        qualityLevel = qualityIndex;
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        this.isFullscreen = isFullscreen;
    }

    public void SetVSync(bool isVSyncEnabled)
    {
        QualitySettings.vSyncCount = isVSyncEnabled ? 1 : 0;
        isVSyncOn = isVSyncEnabled;
    }

    public void SetCameraShake(bool isCameraShakeOn = true)
    {
        isCameraShakeEnabled = isCameraShakeOn;
    }

    public void SetSens(float sens = 1)
    {
        sensitivity = sens;
    }

    public void OnBack()
    {
        PlayerPrefs.SetInt("Fullscreen", Convert.ToInt32(Screen.fullScreen));
        PlayerPrefs.SetInt("Quality", QualitySettings.GetQualityLevel());
        PlayerPrefs.SetInt("VSync", Convert.ToInt32(QualitySettings.vSyncCount > 0));
        PlayerPrefs.SetInt("CameraShake", Convert.ToInt32(isCameraShakeEnabled));
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SoundEffectsVolume", soundEffectsVolume);
        PlayerPrefs.SetFloat("MouseSensitivity", sensitivity);
        PlayerPrefs.Save();
        mainMenuCanvas.SetActive(true);
        gameObject.SetActive(false);
    }

    public void LoadSettings()
    {
        isFullscreen = Convert.ToBoolean(PlayerPrefs.GetInt("Fullscreen", Convert.ToInt32(Screen.fullScreen)));
        SetFullScreen(isFullscreen);

        qualityLevel = PlayerPrefs.GetInt("Quality", QualitySettings.GetQualityLevel());
        SetQuality(qualityLevel);

        isVSyncOn = Convert.ToBoolean(PlayerPrefs.GetInt("VSync", Convert.ToInt32(QualitySettings.vSyncCount > 0)));
        SetVSync(isVSyncOn);

        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0f);
        SetMusicVolume(musicVolume);

        soundEffectsVolume = PlayerPrefs.GetFloat("SoundEffectsVolume", 0f);
        SetSoundEffectVolume(soundEffectsVolume);

        sensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 1f);
        SetSens(sensitivity);

        isCameraShakeEnabled = Convert.ToBoolean(PlayerPrefs.GetInt("CameraShake", 1));
        SetCameraShake(isCameraShakeEnabled);
    }

    private void UpdateUI()
    {
        musicSlider.value = musicVolume;
        soundEffectsSlider.value = soundEffectsVolume;
        qualityDropdown.value = qualityLevel;
        fullscreenToggle.isOn = isFullscreen;
        vsyncToggle.isOn = isVSyncOn;
        sensitivitySlider.value = sensitivity;
        cameraShake.isOn = isCameraShakeEnabled;
    }
}
