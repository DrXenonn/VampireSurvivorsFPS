using TMPro;
using UnityEditor;
using UnityEngine;

public class FontReplacer : MonoBehaviour
{
    public TMP_FontAsset newFont;
    public float sizeIncrement = 10f;
    public bool makeBold = true;
    public float yPositionOffset = 0f;

    [ContextMenu("ReplaceFontInScene")]
    private void ReplaceFontInScene()
    {
        // Get all TextMeshPro objects in the scene
        TMP_Text[] textMeshProObjects = FindObjectsOfType<TMP_Text>();

        // Replace the font, increase size, set boldness, and adjust y position of each TextMeshPro object
        foreach (TMP_Text textMeshPro in textMeshProObjects)
        {
            ReplaceFontIncreaseSizeSetBoldnessAndAdjustYPosition(textMeshPro);
        }
    }

    [ContextMenu("ReplaceFontInPrefabs")]
    private void ReplaceFontInPrefabs()
    {
        // Get all prefabs in the project
        string[] guids = AssetDatabase.FindAssets("t:Prefab");

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            // Find TextMeshPro objects in the prefab
            TMP_Text[] textMeshProObjects = prefab.GetComponentsInChildren<TMP_Text>(true);

            // Replace the font, increase size, set boldness, and adjust y position of each TextMeshPro object
            foreach (TMP_Text textMeshPro in textMeshProObjects)
            {
                ReplaceFontIncreaseSizeSetBoldnessAndAdjustYPosition(textMeshPro);
            }

            // Save the modified prefab
            PrefabUtility.SavePrefabAsset(prefab);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private void ReplaceFontIncreaseSizeSetBoldnessAndAdjustYPosition(TMP_Text textMeshPro)
    {
        Undo.RecordObject(textMeshPro, "Font Replacement");
        textMeshPro.font = newFont;

        // Increase the size of the TextMeshPro object
        textMeshPro.fontSize += sizeIncrement;

        // Set boldness of the TextMeshPro object
        textMeshPro.fontStyle = makeBold ? FontStyles.Bold : FontStyles.Normal;

        // Adjust the y position of the TextMeshPro object
        Vector3 newPosition = textMeshPro.transform.position;
        newPosition.y += yPositionOffset;
        textMeshPro.transform.position = newPosition;

        EditorUtility.SetDirty(textMeshPro);
    }
}
