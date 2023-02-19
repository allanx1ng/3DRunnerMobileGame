using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    private Vector3 currentPosition = new Vector3(-5, 0, 0);

    [SerializeField] private int maxTerrainCount;
    [SerializeField] private List<TerrainData> terrainData = new List<TerrainData>();
    [SerializeField] private Transform terrainHolder;


    // List of all currently placed terrains on the scene
    private List<GameObject> terrains = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {

        while (terrains.Count < maxTerrainCount)
        {
            spawnRandomTerrain();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            spawnRandomTerrain();
        }

    }


    /// <summary>
    /// Spawns a random terrain at the current position
    /// </summary>
    private void spawnRandomTerrain()
    {

        int chooseTerrain = Random.Range(0, terrainData.Count);
        TerrainData td = terrainData[chooseTerrain];
        int terrainInSuccession = Random.Range(td.minInSuccession > 0 ? td.minInSuccession : 1, td.maxInSuccession);
        for (int i = 0; i < terrainInSuccession; i++)
        {
            GameObject terrain = Instantiate(terrainData[chooseTerrain].terrain, currentPosition, Quaternion.identity);
            terrain.transform.SetParent(terrainHolder);
            terrains.Add(terrain);
            currentPosition.x++;

            if (terrains.Count > maxTerrainCount)
            {
                Destroy(terrains[0]);
                terrains.RemoveAt(0);
            }
        }
        


    }

}
