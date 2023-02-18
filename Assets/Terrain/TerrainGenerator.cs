using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    private Vector3 currentPosition = new Vector3(-5, 0, 0);

    [SerializeField] private int maxTerrainCount;
    [SerializeField] private List<TerrainData> terrainData = new List<TerrainData>();


    // List of all currently placed terrains on the scene
    private List<GameObject> terrains = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < maxTerrainCount; i++)
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
        int terrainInSuccession = Random.Range(0, terrainData[chooseTerrain].maxInSuccession);
        for (int i = 0; i < terrainInSuccession; i++)
        {
            GameObject terrain = Instantiate(terrainData[chooseTerrain].terrain, currentPosition, Quaternion.identity);
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
