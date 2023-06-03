using UnityEngine;

public class MinecartController : MonoBehaviour
{
    private Renderer terrainRenderer;
    private Bounds terrainBounds;
    private float minecartVelocity = 25f;
    private float direction = 1;
    public void Initialize(Renderer terrainRenderer, float direction)
    {
        this.terrainRenderer = terrainRenderer;
        terrainBounds = terrainRenderer.bounds;
        this.direction = direction;
    }

    private void Update()
    {
        if (!terrainBounds.Contains(transform.position))
        {
            Destroy(gameObject);
        }

        transform.Translate(new Vector3(minecartVelocity * direction, 0f, 0f) * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider otherCollider) {
        PlayerManager.Instance.DamagePlayerIfHit(otherCollider);
    }
}