using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Terrain Data", menuName = "Terrain Data")]
public class TerrainData : ScriptableObject
{
    public GameObject terrain;
    public int maxInSuccession; // Number of terrains in a row of the same type
    public int minInSuccession; // min number of terrains in a row

    public bool canSpawnBlocks = true;

    

    public List<ObstacleData> spawnableMobs = new List<ObstacleData>();
    public List<ObstacleData> spawnableBlocks = new List<ObstacleData>();
}
