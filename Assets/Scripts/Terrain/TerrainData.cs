using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Terrain Data", menuName = "Terrain Data")]
public class TerrainData : ScriptableObject
{

    public GameObject terrain;
    public TerrainData entranceTerrainData; // this object is responsible for transitioning lighting, etc upon entering a different biome.
    public TerrainData exitTerrainData;

    public int maxInSuccession = 30; // Number of terrains in a row of the same type
    public int minInSuccession = 10; // min number of terrains in a row

    public bool canSpawnBlocks = true;
    public List<ObstacleData> spawnableMobs = new List<ObstacleData>();
    public List<ObstacleData> spawnableBlocks = new List<ObstacleData>();

    public bool isTransitionTerrain = false;
    public string transitionType;

    // Static terrain constants
    public const string TERRAIN_CAVE_ENTRANCE = "CAVE ENTRANCE";
    public const string TERRAIN_CAVE_EXIT = "CAVE EXIT";

    public const string TERRAIN_HELL_ENTRANCE = "HELL ENTRANCE";
    public const string TERRAIN_HELL_EXIT = "HELL EXIT";


}
