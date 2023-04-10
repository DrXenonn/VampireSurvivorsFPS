using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Image border;
    [SerializeField] TextMeshProUGUI upgradeName;

    public void Set(UpgradeData upgradeData)
    {
        icon.sprite = upgradeData.upgradeIcon;
        icon.color = upgradeData.upgradeColor;
        //upgradeName.text = upgradeData.upgradeName;
    }

    public void Clean()
    {
        icon.sprite = null;
        //upgradeName.text = null;

        RemoveSelectEffect();
    }

    public void ApplySelectEffect()
    {
        transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
        icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, 1f);
        border.color = new Color(border.color.r, border.color.g, border.color.b, 1f);
    }
    public void RemoveSelectEffect() 
    {
        transform.localScale = new Vector3(1,1,1);
        icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, 0.5f);
        border.color = new Color(border.color.r, border.color.g, border.color.b, 0.5f);
    }
}