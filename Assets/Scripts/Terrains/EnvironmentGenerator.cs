using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGenerator : MonoBehaviour
{

    [SerializeField] private float leftBoundary = -10f;
    [SerializeField] private float rightBoundary = 10f;

    [SerializeField] private List<GameObject> environmentObjects = new List<GameObject>();

    [SerializeField] private List<GameObject> walls = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
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
        Instantiate(walls[leftWallModel], left, leftRotation);
        Instantiate(walls[leftWallModel], right, rightRotation);


    }
}
