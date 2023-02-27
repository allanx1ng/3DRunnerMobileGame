using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Terrain Data", menuName = "Terrain Data")]
public class TerrainData : ScriptableObject
{
    public GameObject terrain;
    public int maxInSuccession; // Number of terrains in a row of the same type
    public int minInSuccession; // min number of terrains in a row

    public List<GameObject> spawnableMobs = new List<GameObject>();
    public List<GameObject> spawnableBlocks = new List<GameObject>();
}
