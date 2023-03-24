using System.Collections;
using UnityEngine;

public class MinecartSpawner : MonoBehaviour
{
    [SerializeField] private GameObject minecartPrefab;
    
    [SerializeField] private float spawnDistanceThreshold = 100f;
    [SerializeField] private float minTime = 5f;
    [SerializeField] private float maxTime = 10f;
    [SerializeField] private float minecartVelocity = 20f;
    [SerializeField] private float offsetFromEdge = 25;

    private Renderer objectRenderer;
    private Bounds objectBounds;
    private GameObject player;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        objectBounds = objectRenderer.bounds;

        StartCoroutine(WaitForPlayerInitialization());
    }

    private IEnumerator WaitForPlayerInitialization()
    {
        while (PlayerManager.Instance.CurrentPlayer == null)
        {
            yield return null;
        }

        player = PlayerManager.Instance.CurrentPlayer;
        StartCoroutine(SpawnMinecarts());
    }
    private IEnumerator SpawnMinecarts()
    {
        // To start off with a random time taken to spawn a minecart, only does anything if the game starts with the player close to the terrain that spawns
        // the minecarts.
        yield return new WaitForSeconds(Random.Range(1f, 2f));

        while (true)
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, objectBounds.center);
            if (distanceToPlayer <= spawnDistanceThreshold)
            {
                // Determine spawn position and direction
                bool spawnOnLeft = Random.Range(0, 2) == 0;
                float xPos = objectBounds.center.x + (spawnOnLeft ? -objectBounds.extents.x + offsetFromEdge : objectBounds.extents.x - offsetFromEdge);
                Vector3 spawnPosition = new Vector3(xPos, objectBounds.center.y, objectBounds.center.z);
                float direction = spawnOnLeft ? 1f : -1f;

                // Instantiate minecart and set its velocity
                
                Quaternion rotationQuaternion = Quaternion.AngleAxis(spawnOnLeft ? 90f : -90f, Vector3.up); // generate a rotation transform 90f or -90f depending on spawnOnLeft
                GameObject minecartInstance = Instantiate(minecartPrefab, spawnPosition, rotationQuaternion);
                
                Rigidbody minecartRigidbody = minecartInstance.GetComponent<Rigidbody>();
                if (minecartRigidbody == null)
                {
                    minecartRigidbody = minecartInstance.AddComponent<Rigidbody>();
                }
                minecartRigidbody.useGravity = false;
                minecartRigidbody.velocity = new Vector3(minecartVelocity * direction, 0f, 0f);

                // Set the parent of the minecart so that it will be destroyed when the terrain does.
                minecartInstance.transform.parent = transform;

                // Initialize MinecartController with the terrain Renderer
                MinecartController minecartController = minecartInstance.GetComponent<MinecartController>();
                if (minecartController == null)
                {
                    minecartController = minecartInstance.AddComponent<MinecartController>();
                }
                minecartController.Initialize(objectRenderer);

                
            }

            float seconds = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(seconds);
        }
    }

    
}