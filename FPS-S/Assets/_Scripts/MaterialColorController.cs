using System;
using System.Collections;
using UnityEngine;

public class MaterialColorController : MonoBehaviour
{
    [SerializeField] private Material targetMaterial;
    [SerializeField] private Color defaultColor;
    [ColorUsage(true,true)]
    [SerializeField] private Color defaultEmissionColor;

    public static MaterialColorController instance;

    private void Awake()
    {
        instance = this;
    }

    private void OnGameEnd()
    {
        // Reset the material's emission color and color to their default values
        targetMaterial.SetColor("_EmissionColor", defaultEmissionColor);
        targetMaterial.SetColor("_BaseColor", defaultColor);
    }

    public void SetNewColors(Color newColor, Color newEmissionColor)
    {
        // Change the emission color and color to the new values
        targetMaterial.SetColor("_EmissionColor", newEmissionColor);
        targetMaterial.SetColor("_BaseColor", newColor);
    }

    private static void ResetColorsOnGameEnd()
    {
        if (instance != null)
        {
            instance.OnGameEnd();
        }
    }

    private void OnDisable()
    {
        ResetColorsOnGameEnd();
    }
}