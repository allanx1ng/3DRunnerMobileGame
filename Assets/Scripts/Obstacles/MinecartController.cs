using UnityEngine;

public class MinecartController : MonoBehaviour
{
    private Renderer terrainRenderer;
    private Bounds terrainBounds;

    public void Initialize(Renderer terrainRenderer)
    {
        this.terrainRenderer = terrainRenderer;
        terrainBounds = terrainRenderer.bounds;
    }

    private void Update()
    {
        if (!terrainBounds.Contains(transform.position))
        {
            Destroy(gameObject);
        }
    }
}