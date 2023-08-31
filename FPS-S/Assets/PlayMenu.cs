using TMPro;
using UnityEngine;

public class PlayMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private TextMeshProUGUI chaosLevelText;

    private void OnEnable()
    {
        var chaos = PlayerPrefs.GetInt("Chaos", 1);

        chaosLevelText.text = $"Chaos Level: {chaos} / 5";
    }

    public void OnPlay()
    {
        SceneManager.Instance.LoadScene("Game");
    }

    public void OnBack()
    {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
