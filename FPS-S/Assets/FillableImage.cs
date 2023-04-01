using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FillableImage : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI text;
    public bool displayPercentage;

    private void Update()
    {
        float fillAmount = image.fillAmount;

        if (displayPercentage)
        {
            int percentage = Mathf.RoundToInt(fillAmount * 100f);
            text.text = percentage + "%";
        }
        else
        {
            text.text = "";
        }
    }
}