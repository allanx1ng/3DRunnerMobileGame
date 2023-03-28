using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTransition : MonoBehaviour
{
    // Directional Light 
    private LightingStruct targetAmbient;
    private LightingStruct targetDirectional;
    private float transitionDuration = 1.0f; // The duration of the transition effect in seconds

    // Initializer, must be called for script to work
    public void Initialize(LightingStruct targetAmbient, LightingStruct targetDirectional) {
        this.targetAmbient = targetAmbient;
        this.targetDirectional = targetDirectional;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure the object entering the trigger is the player
        {
            StartCoroutine(TransitionEffect());
        }
    }

    private IEnumerator TransitionEffect()
    {
        // Store the initial ambient light color
        Color initialColor = RenderSettings.ambientLight;
        float initialSunIntensity = RenderSettings.sun.intensity;

        float elapsedTime = 0;

        // Lerp the ambient light color from the initial color to the dark color
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionDuration;
            RenderSettings.ambientLight = Color.Lerp(initialColor, targetAmbient.ambientColor, t);
            RenderSettings.sun.intensity = Mathf.Lerp(initialSunIntensity, targetDirectional.directionalIntensity, t);
            yield return null;
        }

        // restore the floating point precision error
        RenderSettings.ambientLight = initialColor;
        RenderSettings.sun.intensity = initialSunIntensity;


    }
}
