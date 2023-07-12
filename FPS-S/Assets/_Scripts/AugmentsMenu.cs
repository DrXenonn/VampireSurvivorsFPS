using TMPro;
using UnityEngine;

public class AugmentsMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private TextMeshProUGUI tmp;

    private void OnEnable()
    {
        SetCrystalsUI();
    }

    public void SetCrystalsUI()
    {
        tmp.text = PlayerPrefs.GetInt("Crystals").ToString();
    }

    public void OnBack()
    {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
