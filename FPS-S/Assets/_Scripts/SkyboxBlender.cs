using UnityEngine;

public class SkyboxBlender : MonoBehaviour
{
    public float transitionTime;
    public Material dayMaterial;
    public Material nightMaterial;
    public Material blendMaterial; 
    public float transitionDuration = 3f;
    public Light globalLight;
    public float targetLightIntensity;
    public float targetTemperature;
    public Quaternion targetRotation;
    
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
        
        targetMaterial = new Material(blendMaterial);
        
        CopyMaterialProperties(dayMaterial, targetMaterial);
        
        RenderSettings.skybox = targetMaterial;
        
        Invoke(nameof(StartTransitionNoParam), transitionTime);
        
        InvokeRepeating(nameof(UpdateScript),transitionTime,0.1f);
    }

    private void StartTransitionNoParam()
    {
        StartTransition(dayMaterial, blendMaterial, 0f);
    }


    private void UpdateScript()
    {
        if (isTransitioning)
        {
            transitionTimer += 0.1f;
            
            float progress = Mathf.Clamp01(transitionTimer / transitionDuration);
            
            LerpMaterialProperties(dayMaterial, nightMaterial, targetMaterial, progress);
            
            globalLight.intensity = Mathf.Lerp(startingLightIntensity, targetLightIntensity, progress);
            globalLight.colorTemperature = Mathf.Lerp(startingTemperature, targetTemperature, progress);
            //globalLight.transform.rotation = Quaternion.Slerp(startingRotation, targetRotation, progress);
            
            RenderSettings.ambientIntensity = Mathf.Lerp(1, ambientIntensity, progress);
            RenderSettings.reflectionIntensity = Mathf.Lerp(1, reflectionsIntensity, progress);
            globalLight.shadowStrength = Mathf.Lerp(1, 0, progress);
            
            
            if (progress >= 1f)
            {
                isTransitioning = false;
                
            }
        }
    }

    public void StartTransition(Material startMaterial, Material endMaterial, float transitionValue)
    {
        transitionTimer = 0f;
        
        targetMaterial.SetFloat(CubemapTransition, transitionValue);
        
        CopyMaterialProperties(startMaterial, targetMaterial);
        
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
        targetMaterial.SetColor(TintColor, sourceMaterial.GetColor(TintColor));
        targetMaterial.SetFloat(Exposure, sourceMaterial.GetFloat(Exposure));
        targetMaterial.SetFloat(FogIntensity, sourceMaterial.GetFloat(FogIntensity));
        targetMaterial.SetFloat(FogHeight, sourceMaterial.GetFloat(FogHeight));
        targetMaterial.SetFloat(FogSmoothness, sourceMaterial.GetFloat(FogSmoothness));
        targetMaterial.SetFloat(FogFill, sourceMaterial.GetFloat(FogFill));
    }
}
