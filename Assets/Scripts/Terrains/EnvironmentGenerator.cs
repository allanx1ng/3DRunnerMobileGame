using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGenerator : MonoBehaviour
{

    private float environmentMargin = 15f;
    [SerializeField] private float leftBoundary = -13f;
    [SerializeField] private float rightBoundary = 13f;

    [SerializeField] private List<GameObject> environmentObjects = new List<GameObject>();

    [SerializeField] private List<GameObject> walls = new List<GameObject>();


    private int spawnAttempts = 5;

    private Renderer objectRenderer;
    private Bounds objectBounds;
    

    // Start is called before the first frame update
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        objectBounds = objectRenderer.bounds;
        SpawnWalls();
    }

    // Update is called once per frame
    void Update()
    {
        
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

        Quaternion leftRotation = Quaternion.Euler(-90, Random.Range(0, 4) * 90, 0); 
        Quaternion rightRotation = Quaternion.Euler(-90, Random.Range(0, 4) * 90, 0); 
        GameObject leftWall = Instantiate(walls[leftWallModel], left, leftRotation);
        GameObject rightWall = Instantiate(walls[leftWallModel], right, rightRotation);

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
                maxX = currentPosition.x + leftBoundary;
            } else {
                // rightBoundary to right
                minX = currentPosition.x + rightBoundary;
                maxX = currentPosition.x + 50f;
            }

            // set the correct min and max values (outside of the 3 lanes)
            Vector3 candidatePosition = new Vector3(Random.Range(minX, maxX), currentPosition.y + 0.5f, currentPosition.z);
            
            GameObject environmentObjectPrefab = ChooseEnvironmentObject();
            // Bounds environmentBounds = BoundsHelper.GetBounds(environmentObjectPrefab);
            // float minDistance = Mathf.Max(environmentBounds.extents.x, environmentBounds.extents.z) + environmentMargin; // arbitrary choose the larger width vs length (don't look at height)
            float minDistance = environmentMargin;
            if (CanSpawn(candidatePosition, minDistance, prevTerrain)) {
                Quaternion rotation = Quaternion.Euler(-90, 0, 0); 
                GameObject spawnedEnvironmentObject = Instantiate(environmentObjectPrefab, candidatePosition, rotation);
                spawnedEnvironmentObject.transform.parent = transform;
            }

        }

    }

    private GameObject ChooseEnvironmentObject() {
        int idx = Random.Range(0, environmentObjects.Count);
        return environmentObjects[idx];
    }
    private bool CanSpawn(Vector3 candidatePosition, float minDistance, GameObject prevTerrain) {
        if (prevTerrain == null) return true; // can always spawn if there was no previous terrain
        
        // loop through each of the previous terrain's children and check if they are spaced enough apart
        foreach (Transform childTransform in prevTerrain.transform) {
            if (childTransform.gameObject.tag != "Default Environment") continue;
            if (Vector3.Distance(childTransform.position, candidatePosition) < minDistance) return false;
        }

        // loop through all of the current terrain's children (what env objects are already spawned)
        foreach (Transform childTransform in transform) {
            if (childTransform.gameObject.tag != "Default Environment") continue;
            if (Vector3.Distance(childTransform.position, candidatePosition) < minDistance) return false;
        }

        return true;
    }
}
