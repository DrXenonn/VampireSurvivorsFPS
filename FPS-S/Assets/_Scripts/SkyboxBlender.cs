using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class SkyboxBlender : MonoBehaviour
{
    public Material dayMaterial; // Assign the Day material in the Inspector
    public Material nightMaterial; // Assign the Night material in the Inspector
    public Material blendMaterial; // Assign the Blend material in the Inspector
    public float transitionDuration = 3f; // Adjust the duration of the transition
    public Light globalLight;
    public float targetLightIntensity;
    public float targetTemperature;
    public Quaternion targetRotation;
    

    // Property names
    private static readonly string TintColor = "_TintColor";
    private static readonly string Exposure = "_Exposure";
    private static readonly string FogIntensity = "_FogIntensity";
    private static readonly string FogHeight = "_FogHeight";
    private static readonly string FogSmoothness = "_FogSmoothness";
    private static readonly string FogFill = "_FogFill";
    private static readonly string CubemapTransition = "_CubemapTransition";

    private Material targetMaterial;
    private float transitionTimer;
    private bool isTransitioning;

    private float startingLightIntensity;
    private float startingTemperature;
    private Quaternion startingRotation;
    public float ambientIntensity;
    public float reflectionsIntensity;

    void Start()
    {
        startingLightIntensity = globalLight.intensity;
        startingTemperature = globalLight.colorTemperature;
        startingRotation = globalLight.transform.rotation;

        // Create a new material instance for the target material
        targetMaterial = new Material(blendMaterial);

        // Set the initial properties of the target material to match the Day material
        CopyMaterialProperties(dayMaterial, targetMaterial);

        // Set the target material as the skybox material
        RenderSettings.skybox = targetMaterial;

        // Start the transition from Day to Blend
        StartTransition(dayMaterial, blendMaterial, 0f);
        
        InvokeRepeating(nameof(UpdateScript),1f,0.1f);
    }


    private void UpdateScript()
    {
        // Check if a transition is in progress
        if (isTransitioning)
        {
            // Update the transition timer
            transitionTimer += 0.1f;

            // Calculate the transition progress (0 to 1)
            float progress = Mathf.Clamp01(transitionTimer / transitionDuration);

            // Interpolate the properties between the start and end materials based on the progress
            LerpMaterialProperties(dayMaterial, nightMaterial, targetMaterial, progress);
            
            globalLight.intensity = Mathf.Lerp(startingLightIntensity, targetLightIntensity, progress);
            globalLight.colorTemperature = Mathf.Lerp(startingTemperature, targetTemperature, progress);
            //globalLight.transform.rotation = Quaternion.Slerp(startingRotation, targetRotation, progress);
            
            RenderSettings.ambientIntensity = Mathf.Lerp(1, ambientIntensity, progress);
            RenderSettings.reflectionIntensity = Mathf.Lerp(1, reflectionsIntensity, progress);
            globalLight.shadowStrength = Mathf.Lerp(1, 0, progress);
            
            
            // Check if the transition is complete
            if (progress >= 1f)
            {
                // Stop transitioning
                isTransitioning = false;
                
            }
        }
    }

    public void StartTransition(Material startMaterial, Material endMaterial, float transitionValue)
    {
        // Reset the transition timer
        transitionTimer = 0f;

        // Set the Transition property of the target material
        targetMaterial.SetFloat(CubemapTransition, transitionValue);

        // Assign the start material properties to the target material
        CopyMaterialProperties(startMaterial, targetMaterial);

        // Start transitioning from the start material to the end material
        isTransitioning = true;
    }

    private void LerpMaterialProperties(Material startMaterial, Material endMaterial, Material targetMaterial, float t)
    {
        targetMaterial.SetColor(TintColor, Color.Lerp(startMaterial.GetColor(TintColor), endMaterial.GetColor(TintColor), t));
        targetMaterial.SetFloat(Exposure, Mathf.Lerp(startMaterial.GetFloat(Exposure), endMaterial.GetFloat(Exposure), t));
        targetMaterial.SetFloat(FogIntensity, Mathf.Lerp(startMaterial.GetFloat(FogIntensity), endMaterial.GetFloat(FogIntensity), t));
        targetMaterial.SetFloat(FogHeight, Mathf.Lerp(startMaterial.GetFloat(FogHeight), endMaterial.GetFloat(FogHeight), t));
        targetMaterial.SetFloat(FogSmoothness, Mathf.Lerp(startMaterial.GetFloat(FogSmoothness), endMaterial.GetFloat(FogSmoothness), t));
        targetMaterial.SetFloat(FogFill, Mathf.Lerp(startMaterial.GetFloat(FogFill), endMaterial.GetFloat(FogFill), t));

        // Set the CubemapTransition property based on the progress value
        targetMaterial.SetFloat(CubemapTransition, Mathf.Lerp(0f, 1f, t));
    }

    private void ApplyEffectsLate(float progress = 5)
    {
        RenderSettings.ambientIntensity = Mathf.Lerp(RenderSettings.ambientIntensity, ambientIntensity, progress);
        RenderSettings.reflectionIntensity = Mathf.Lerp(RenderSettings.reflectionIntensity, reflectionsIntensity, progress);
        globalLight.shadowStrength = Mathf.Lerp(globalLight.shadowStrength, 0, progress);
    }

    private void CopyMaterialProperties(Material sourceMaterial, Material targetMaterial)
    {
        // Copy the properties from the source material to the target material
        targetMaterial.SetColor(TintColor, sourceMaterial.GetColor(TintColor));
        targetMaterial.SetFloat(Exposure, sourceMaterial.GetFloat(Exposure));
        targetMaterial.SetFloat(FogIntensity, sourceMaterial.GetFloat(FogIntensity));
        targetMaterial.SetFloat(FogHeight, sourceMaterial.GetFloat(FogHeight));
        targetMaterial.SetFloat(FogSmoothness, sourceMaterial.GetFloat(FogSmoothness));
        targetMaterial.SetFloat(FogFill, sourceMaterial.GetFloat(FogFill));
        // Add more properties as needed
    }
}
