using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    private Vector3 currentPosition = new Vector3(0, 0, -5);

    [SerializeField] private int maxTerrainCount;
    [SerializeField] private List<TerrainData> terrainData = new List<TerrainData>();
    [SerializeField] private Transform terrainHolder;

    [SerializeField] private GameObject player; // player holder


    // List of all currently placed terrains on the scene
    private List<GameObject> terrains = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {

        while (terrains.Count < maxTerrainCount)
        {
            SpawnRandomTerrain();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        while (IsTerrainSpawnNeeded())
        {
            SpawnRandomTerrain();
        }

        DeleteTerrain();

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
            terrain.transform.SetParent(terrainHolder);
            terrains.Add(terrain);
            currentPosition.z++;
        }
        
    }

    // Deletes the old terrain if old terrain surpasses maxTerrainCount, and it is a certain amount of distance behind the player
    private void DeleteTerrain() {

        while (IsPlayerPastTerrain())
        {
            Destroy(terrains[0]);
            terrains.RemoveAt(0);
        }

    }

    private bool IsPlayerPastTerrain() {
        
        return player.transform.position.z - 5 > terrains[0].transform.position.z;
    }

    private bool IsTerrainSpawnNeeded() {
        return terrains.Count < maxTerrainCount;
    }

}
