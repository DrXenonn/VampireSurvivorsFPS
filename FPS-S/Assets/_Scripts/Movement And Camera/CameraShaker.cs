using Cinemachine;
using UnityEngine;
using System.Collections;
public static class CameraShaker
{
    public static void ShakeCamera(float amplitude = 0.4f, float frequency = 0.1f, float duration = 0.175f)
    {
        // Get the main camera
        CinemachineBrain brain = Camera.main.GetComponent<CinemachineBrain>();
        if (brain != null)
        {
            // Get the virtual camera
            CinemachineVirtualCamera vcam = brain.ActiveVirtualCamera as CinemachineVirtualCamera;
            if (vcam != null)
            {
                // Create a noise settings object and set its properties
                CinemachineBasicMultiChannelPerlin noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                if (noise != null)
                {
                    noise.m_AmplitudeGain = amplitude;
                    noise.m_FrequencyGain = frequency;

                    // Stop any previous shake coroutine and start a new one
                    vcam.StopAllCoroutines();
                    vcam.StartCoroutine(ShakeCoroutine(noise, duration));
                }
            }
        }
    }

    private static IEnumerator ShakeCoroutine(CinemachineBasicMultiChannelPerlin noise, float duration)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            // Update the noise properties
            noise.m_AmplitudeGain = Mathf.Lerp(noise.m_AmplitudeGain, 0.0f, Time.deltaTime / duration);
            noise.m_FrequencyGain = Mathf.Lerp(noise.m_FrequencyGain, 0.0f, Time.deltaTime / duration);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Reset the noise properties
        noise.m_AmplitudeGain = 0.0f;
        noise.m_FrequencyGain = 0.0f;
    }
}
