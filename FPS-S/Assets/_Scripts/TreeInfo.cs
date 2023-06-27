using UnityEngine;
using TMPro;

public class TreeInfo : MonoBehaviour
{
    [SerializeField] private GameObject uiElement;
    private Transform parent;
    private TextMeshProUGUI tmpName;
    private TextMeshProUGUI tmpInfo;
    private string originalName;
    private Color originalColor;
    private string originalInfo;

    private void Awake()
    {
        parent = transform.parent.parent;
        var textMeshPros = GetTextMeshPros(parent);
        tmpName = textMeshPros[0];
        tmpInfo = textMeshPros[1];
        originalName = tmpName.text;
        originalColor = tmpName.color;
        originalInfo = tmpInfo.text;
    }

    public void OnHover()
    {
        var textMeshPros = GetTextMeshPros(uiElement.transform);
        tmpName.text = textMeshPros[0].text;
        tmpName.color = textMeshPros[0].color;
        tmpInfo.text = textMeshPros[1].text;
    }

    public void OnUnHover()
    {
        tmpName.text = originalName;
        tmpName.color = originalColor;
        tmpInfo.text = originalInfo;
    }

    private static TextMeshProUGUI[] GetTextMeshPros(Component targetTransform)
    {
        return targetTransform.GetComponentsInChildren<TextMeshProUGUI>();
    }
}