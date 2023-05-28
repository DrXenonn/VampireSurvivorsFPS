using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;
public class EffectIndicator : MonoBehaviour
{
    [SerializeField] private Volume volume;
    private Vignette vignette;

    [SerializeField] private Color defaultColor;
    [SerializeField] private Color redColor;
    [SerializeField] private Color greenColor;
    [SerializeField] private Color yellowColor;
    [SerializeField] private Color blueColor;


    private float defaultIntensity;
    private float defaultSmoothness;

    private bool isEffectActive = false;

    private void Start()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out vignette);

        defaultIntensity = vignette.intensity.value;
        defaultSmoothness = vignette.smoothness.value;
    }

    public void SetVignetteEffect(Color color, float transitionDuration)
    {
        if (!isEffectActive)
        {
            if (vignette != null)
            {
                vignette.color.Override(color);
                vignette.intensity.Override(defaultIntensity * 1.25f);
                vignette.smoothness.Override(defaultSmoothness * 1.25f);

                StartCoroutine(FadeOutVignette(transitionDuration));
            }

            isEffectActive = true;
        }
    }

    private IEnumerator FadeOutVignette(float transitionDuration)
    {
        yield return new WaitForSeconds(transitionDuration);

        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            float progress = elapsedTime / transitionDuration;

            vignette.color.Override(Color.Lerp(vignette.color.value, defaultColor, progress));
            vignette.intensity.Override(Mathf.Lerp(vignette.intensity.value, defaultIntensity, progress * 1.25f));
            vignette.smoothness.Override(Mathf.Lerp(vignette.smoothness.value, defaultSmoothness, progress * 1.25f));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        vignette.color.Override(defaultColor);
        vignette.intensity.Override(defaultIntensity);
        vignette.smoothness.Override(defaultSmoothness);

        isEffectActive = false;
    }

    public void OnTakeDamage(Component sender, object data)
    {
        SetVignetteEffect(redColor, 0.5f);
    }

    public void OnHeal(Component sender, object data)
    {
        SetVignetteEffect(greenColor, 0.15f);
    }

    public void OnBuff(Component sender, object data)
    {
        SetVignetteEffect(yellowColor, 1f);
    }

    public void OnRestoreMana(Component sender, object data) 
    {
        SetVignetteEffect(blueColor, 0.25f);
    }
}
