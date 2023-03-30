using System.Collections;
using UnityEngine;

public class LightingManager : MonoBehaviour
{
    private Coroutine currentTransitionCoroutine;

    public void TransitionAmbientLight(LightingStruct targetLighting, float duration)
    {
        if (currentTransitionCoroutine != null)
        {
            StopCoroutine(currentTransitionCoroutine);
        }
        currentTransitionCoroutine = StartCoroutine(TransitionAmbientLightCoroutine(targetLighting, duration));
    }

    private IEnumerator TransitionAmbientLightCoroutine(LightingStruct targetLighting, float duration)
    {
        Color initialAmbientColor = RenderSettings.ambientLight;
        float initialDirectionalIntensity = RenderSettings.sun.intensity;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            RenderSettings.ambientLight = Color.Lerp(initialAmbientColor, targetLighting.ambientColor, t);
            RenderSettings.sun.intensity = Mathf.Lerp(initialDirectionalIntensity, targetLighting.directionalIntensity, t);
            yield return null;
        }

        RenderSettings.ambientLight = targetLighting.ambientColor;
        currentTransitionCoroutine = null;
    }
}