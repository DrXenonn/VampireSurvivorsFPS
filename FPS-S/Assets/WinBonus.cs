using System;
using TMPro;
using UnityEngine;

public class WinBonus : MonoBehaviour
{
    [SerializeField] private int bonusCrystals;
    [SerializeField] private TextMeshProUGUI tmp;

    private void Start()
    {
        tmp.text = $"*{bonusCrystals}";
    }

    public void OnContinue()
    {
        PlayerPrefs.SetInt("Crystals", PlayerPrefs.GetInt("Crystals") + bonusCrystals);
        SceneManager.Instance.LoadScene("Main Menu");
    }
}
