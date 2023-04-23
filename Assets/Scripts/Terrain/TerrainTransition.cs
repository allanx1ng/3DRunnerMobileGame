using UnityEngine;

public class TerrainTransition : MonoBehaviour
{
    // Directional Light 
    private LightingStruct targetLighting;
    private float transitionDuration = 0.5f;  // The duration of the transition effect in seconds
    private LightingManager lightingManager;

    // Initializer, must be called for script to work
    public void Initialize(LightingStruct targetLighting)
    {
        this.targetLighting = targetLighting;
    }

    private void Start()
    {
        lightingManager = LightingManager.Instance;
        if (lightingManager == null)
        {
            Debug.LogError("LightingManager not found in the scene.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure the object entering the trigger is the player
        {
            if (lightingManager != null)
            {
                lightingManager.TransitionAmbientLight(targetLighting, transitionDuration);
            }
        }
    }
}