using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGenerator : MonoBehaviour
{

    // How far away an environment object MUST at least be from another + its bounding box extents
    private float environmentMargin = 1f;
    private float leftBoundary = -10f;
    private float rightBoundary = 10f;
    private float widthFence = 5f;

    [SerializeField] private List<GameObject> environmentObjects = new List<GameObject>();

    [SerializeField] private List<GameObject> walls = new List<GameObject>();

    [SerializeField] private int spawnAttempts = 6;

    private Renderer objectRenderer;
    private Bounds objectBounds;
    
    private const float SQRT_2 = 1.41422f;

    // Start is called before the first frame update
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        objectBounds = objectRenderer.bounds;
        SpawnWalls();
    }


    private void SpawnWalls() {
        
        // spawn the walls
        int leftWallModel = Random.Range(0, walls.Count);
        int rightWallModel = Random.Range(0, walls.Count);
        Vector3 left = transform.position;
        left.x += leftBoundary;
        left.y = 1f;

        Vector3 right = transform.position;
        right.x += rightBoundary;
        right.y = 1f;

        Quaternion leftRotation = Quaternion.Euler(0, 0, 0); 
        Quaternion rightRotation = Quaternion.Euler(0, 0, 0); 
        GameObject leftWall = Instantiate(walls[leftWallModel], left, leftRotation);
        GameObject rightWall = Instantiate(walls[leftWallModel], right, rightRotation);

        leftWall.tag = "Default Environment";
        rightWall.tag = "Default Environment";

        leftWall.transform.parent = transform;
        rightWall.transform.parent = transform;
    }

    // Spawns in environmental objects restricted based on the previous Terrain object
    public void SpawnEnvironment(GameObject prevTerrain) {

        if (environmentObjects.Count == 0) {
            // Debug.Log("[WARNING]: No environmental objects for this particular game object! " + gameObject.name);
            return;
        }

        for (int i = 0; i < spawnAttempts; i++) {
            Vector3 currentPosition = transform.position;
            // float minX = currentPosition.x - objectBounds.extents.x;
            // float maxX = currentPosition.x + objectBounds.extents.x;
            float minX = -50f;
            float maxX = 50f;

            if (Random.Range(0, 2) == 0) {
                // left to leftBoundary
                minX = currentPosition.x - 50f;
                maxX = currentPosition.x + leftBoundary - widthFence;
            } else {
                // rightBoundary to right
                minX = currentPosition.x + rightBoundary + widthFence;
                maxX = currentPosition.x + 50f;
            }

            // set the correct min and max values (outside of the 3 lanes)
            
            int tilesX = (int) ((maxX - minX) / 10f);
            float randomX = Random.Range(0, tilesX + 1) * 10 + minX;
            Vector3 candidatePosition = new Vector3(randomX, currentPosition.y + 0.5f, currentPosition.z);
            
            GameObject environmentObjectPrefab = ChooseEnvironmentObject();

            Bounds environmentBounds = BoundsHelper.GetBounds(environmentObjectPrefab);
            float minDistanceFromObject = Mathf.Max(environmentBounds.extents.x * SQRT_2, environmentBounds.extents.z * SQRT_2) + environmentMargin; // arbitrary choose the larger width vs length (don't look at height)

            if (CanSpawn(candidatePosition, minDistanceFromObject, prevTerrain)) {
                GameObject spawnedEnvironmentObject = Instantiate(environmentObjectPrefab, candidatePosition, Quaternion.identity);
                spawnedEnvironmentObject.tag = "Default Environment";
                spawnedEnvironmentObject.transform.parent = transform;
            }

        }

    }

    private GameObject ChooseEnvironmentObject() {
        int idx = Random.Range(0, environmentObjects.Count);
        return environmentObjects[idx];
    }
    private bool CanSpawn(Vector3 candidatePosition, float minDistanceFromObject, GameObject prevTerrain) {
        if (prevTerrain == null) return true; // can always spawn if there was no previous terrain

        // loop through each of the previous terrain's children and check if they are spaced enough apart
        foreach (Transform childTransform in prevTerrain.transform) {
            if (childTransform.gameObject.CompareTag("Default Environment")) continue;
            Bounds bounds = BoundsHelper.GetBounds(childTransform.gameObject);
            float biggerDimension = Mathf.Max(bounds.extents.x, bounds.extents.z);
            float minDistanceOverall = minDistanceFromObject + (biggerDimension * SQRT_2);
            if (Vector3.Distance(childTransform.position, candidatePosition) < minDistanceOverall) return false;
        } 

        // loop through all of the current terrain's children (what env objects are already spawned)
        foreach (Transform childTransform in transform) {
            if (childTransform.gameObject.CompareTag("Default Environment")) continue;
            Bounds bounds = BoundsHelper.GetBounds(childTransform.gameObject);
            float biggerDimension = Mathf.Max(bounds.extents.x, bounds.extents.z);
            float minDistanceOverall = minDistanceFromObject + (biggerDimension * SQRT_2);
            if (Vector3.Distance(childTransform.position, candidatePosition) < minDistanceOverall) return false;
        }

        return true;
    }
}
