using System.Collections;
using UnityEngine;

public class MinecartSpawner : MonoBehaviour
{
    [SerializeField] private GameObject minecartPrefab;
    
    [SerializeField] private float spawnDistanceThreshold = 50f;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private float minecartVelocity = 5f;

    private Renderer objectRenderer;
    private Bounds objectBounds;
    private GameObject player;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        objectBounds = objectRenderer.bounds;
        player = PlayerManager.Instance.CurrentPlayer;

        StartCoroutine(SpawnMinecarts());
    }

    private IEnumerator SpawnMinecarts()
    {
        while (true)
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, objectBounds.center);
            if (distanceToPlayer <= spawnDistanceThreshold)
            {
                // Determine spawn position and direction
                bool spawnOnLeft = Random.Range(0, 2) == 0;
                float xPos = objectBounds.center.x + (spawnOnLeft ? -objectBounds.extents.x : objectBounds.extents.x);
                Vector3 spawnPosition = new Vector3(xPos, objectBounds.center.y, objectBounds.center.z);
                float direction = spawnOnLeft ? 1f : -1f;

                // Instantiate minecart and set its velocity
                GameObject minecartInstance = Instantiate(minecartPrefab, spawnPosition, Quaternion.identity);
                Rigidbody minecartRigidbody = minecartInstance.GetComponent<Rigidbody>();
                if (minecartRigidbody == null)
                {
                    minecartRigidbody = minecartInstance.AddComponent<Rigidbody>();
                }
                minecartRigidbody.useGravity = false;
                minecartRigidbody.velocity = new Vector3(minecartVelocity * direction, 0f, 0f);

                // Initialize MinecartController with the terrain Renderer
                MinecartController minecartController = minecartInstance.GetComponent<MinecartController>();
                if (minecartController == null)
                {
                    minecartController = minecartInstance.AddComponent<MinecartController>();
                }
                minecartController.Initialize(objectRenderer);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    
}