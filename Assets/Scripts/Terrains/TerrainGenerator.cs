using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    private Vector3 currentPosition = new Vector3(0, 0, -5);
    [SerializeField] private int numOfObstaclesOnRow = 3;
    // The terrain generator needs to know of the obstacle generator to generate new obstacles / delete obstacles when terrain is off screen.
    [SerializeField] private GameObject obstacleGeneratorObject;
    private ObstacleGenerator obstacleGenerator; // cached script of the obstacleGeneratorObject
    [SerializeField] private int maxTerrainCount;
    [SerializeField] private Transform terrainHolder;
    [SerializeField] private GameObject player; // player holder
    [SerializeField] private List<TerrainData> terrainData = new List<TerrainData>();

    // List of all currently placed terrains on the scene
    private List<GameObject> terrains = new List<GameObject>();
    private GameObject prevTerrain = null;

    // Start is called before the first frame update
    void Start()
    {

        SpawnTerrainUntilMaxTerrainCount();
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnTerrainUntilMaxTerrainCount();
        DeleteTerrain();
    }
    
    // Awake is called before Start, so we can cache the obstacleGeneratorObject here
    void Awake() {
        obstacleGenerator = obstacleGeneratorObject.GetComponent<ObstacleGenerator>();
    }

    // Spawns terrain until the maxTerrainCount is reached
    void SpawnTerrainUntilMaxTerrainCount() {
        while (terrains.Count < maxTerrainCount)
        {
            SpawnRandomTerrain();
        }
    }

    /// <summary>
    /// Spawns a random terrain at the current position
    /// </summary>
    private void SpawnRandomTerrain()
    {
        int chooseTerrain = Random.Range(0, terrainData.Count);
        TerrainData td = terrainData[chooseTerrain];
        int terrainInSuccession = Random.Range(td.minInSuccession > 0 ? td.minInSuccession : 1, td.maxInSuccession);
        for (int i = 0; i < terrainInSuccession; i++)
        {
            GameObject terrain = Instantiate(terrainData[chooseTerrain].terrain, currentPosition, Quaternion.identity);
            
            obstacleGenerator.GenerateObstacles(currentPosition, numOfObstaclesOnRow, td);
            
            EnvironmentGenerator environmentGenerator = terrain.GetComponent<EnvironmentGenerator>();
            if (environmentGenerator) environmentGenerator.SpawnEnvironment(prevTerrain);

            terrain.transform.SetParent(terrainHolder);
            terrains.Add(terrain);
            currentPosition.z += 5;
            prevTerrain = terrain;
        }
        
    }

    /// <summary>
    /// Deletes the old terrain if old terrain surpasses maxTerrainCount, and it is a certain amount of distance behind the player
    /// </summary>
    private void DeleteTerrain() {

        while (IsPlayerPastTerrain())
        {
            
            Destroy(terrains[0]);
            terrains.RemoveAt(0);
            obstacleGenerator.DeleteObstacleRow();
        }

    }

    // Checks if the player is past the terrain
    private bool IsPlayerPastTerrain() {
        
        return player.transform.position.z - 15 > terrains[0].transform.position.z;
    }
    
    // Checks if the terrain needs to be spawned
    private bool IsTerrainSpawnNeeded() {
        return terrains.Count < maxTerrainCount;
    }

}
