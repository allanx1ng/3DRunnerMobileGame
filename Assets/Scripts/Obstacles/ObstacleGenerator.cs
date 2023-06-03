using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [TextArea]
    public string SpawningComment = "This handles the spawning behaviour of the obstacles, the minimum and maximum distance between clusters, and size of clusters";

    public const int MIN_SIZE_MARGIN = 8;
    public const int MAX_SIZE_MARGIN = 18;
    public const int MIN_SIZE_CLUSTER = 3;
    public const int MAX_SIZE_CLUSTER = 12;

    private int currentSizeCluster = MIN_SIZE_CLUSTER;
    private int currentSizeMargin = MIN_SIZE_MARGIN;
    private int clusterIndex = 0; // the index on the current cluster
    private int marginIndex = 0; // the index when not spawning a cluster, 
    
    
    [TextArea]
    public string ChanceComment = "Inbetween clusters, there will be areas of empty space in the margins. Here it can spawn things based on these probabilities";
    public float sizeOfBlock = 5f; // The size of the block should include the MARGIN that a block should have between other blocks
    public float chanceForNothing = 85f;
    public float chanceForMob = 10f;
    public float chanceForBlock = 5f;

    [SerializeField] private List<ObstacleData> commonBlockData = new List<ObstacleData>(); // blocks that are spawnable in all biomes
    [SerializeField] private List<ObstacleData> commonMobData = new List<ObstacleData>();
    private List<GameObject> obstacleRows = new List<GameObject>();
    [SerializeField] private GameObject obstacleHolder;
    private int countOfGeneratedRows = 0;
    public int playerBufferBeforeSpawn = 7;
    private void GenerateObstacle(Vector3 position, GameObject obstaclePrefab, GameObject obstacleRow) {
        // Generate an obstacle at the given position
        Quaternion rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f)); // face backwards
        GameObject obstacle = Instantiate(obstaclePrefab, position, rotation);
        obstacle.transform.SetParent(obstacleRow.transform);
    }



    public void GenerateObstacles(Vector3 position, int amount, TerrainData terrainData) {
        
        

        // Generate multiple obstacles centered at the given position, creating a row of obstacles on the x-axis
        float startX = 0 - (((float) amount - 1) / 2 * sizeOfBlock);    

        GameObject obstacleRow = new GameObject("Obstacle Row " + countOfGeneratedRows);
        obstacleRow.transform.SetPositionAndRotation(position, Quaternion.identity);
        obstacleRow.transform.SetParent(obstacleHolder.transform);
        obstacleRows.Add(obstacleRow);

        if (playerBufferBeforeSpawn > 0) {
            playerBufferBeforeSpawn--;
            return;
        }

        // Dont generate obstacles for objects with no children (roads)
        if (terrainData.canSpawnBlocks) {
            if (marginIndex < currentSizeMargin) {
                // spawns an object (or not) based on probabilities for the void space
                spawnRowMargin(startX, amount, terrainData, position, obstacleRow);
                marginIndex++;
            } else if (clusterIndex < currentSizeCluster) {
                spawnRowCluster(startX, amount, terrainData, position, obstacleRow);
                clusterIndex++;
            } else {
                resetIndices();
            }
        } else {
            resetIndices();
        }
        
        countOfGeneratedRows++;
    }

    private void resetIndices() {
        // reset the indices
        clusterIndex = 0;
        marginIndex = 0;

        currentSizeCluster = Random.Range(MIN_SIZE_CLUSTER, MAX_SIZE_CLUSTER + 1);
        currentSizeMargin = Random.Range(MIN_SIZE_MARGIN, MAX_SIZE_MARGIN + 1);
    }

    private void spawnRowCluster(float startX, int amount, TerrainData terrainData, Vector3 position, GameObject obstacleRow) {
        for (int i = 0; i < amount; i++) {
            float newX = startX + (i * sizeOfBlock);
            GameObject block = ChooseBlock(terrainData);
            block.tag = "Block";
            GenerateObstacle(new Vector3(newX, position.y + 3, position.z), block, obstacleRow);
        }
        
    }

    private void spawnRowMargin(float startX, int amount, TerrainData terrainData, Vector3 position, GameObject obstacleRow) {
        // Chance of spawning nothing vs. block vs. mob
        float totalChance = chanceForBlock + chanceForMob + chanceForNothing;

        // spawns an object (or not) based on probabilities for the void space
        
        for (int i = 0; i < amount; i++) {
            float randomNumber = Random.Range(0f, totalChance);
            float newX = startX + (i * sizeOfBlock);
            if (randomNumber <= chanceForNothing) {
                // do nothing
                return;
            } else if (randomNumber <= chanceForMob + chanceForNothing) {
                // spawn mob
                
                GameObject mob = ChooseMob(terrainData);
                mob.tag = "Mob";
                GenerateObstacle(new Vector3(newX, position.y + 1, position.z), mob, obstacleRow);
            } else {
                // spawn block
                GameObject block = ChooseBlock(terrainData);
                block.tag = "Block";
                GenerateObstacle(new Vector3(newX, position.y + 3, position.z), block, obstacleRow);
            }
        }
    }

    public GameObject ChooseBlock(TerrainData terrainData) {
        List<ObstacleData> blockList = terrainData.spawnableBlocks;
        ObstacleData obstacleData = ChooseWeightedObstacleData(commonBlockData, blockList);
        return obstacleData.gameObject;
    }

    public GameObject ChooseMob(TerrainData terrainData) {
        List<ObstacleData> mobList = terrainData.spawnableMobs;
        ObstacleData obstacleData = ChooseWeightedObstacleData(commonMobData);
        return obstacleData.gameObject;
    }
    
    public void DeleteObstacleRow() {
        if (obstacleRows.Count < 1) return;
        Destroy(obstacleRows[0]);
        obstacleRows.RemoveAt(0);
    }

    private float GetTotalChance(params List<ObstacleData>[] list) {
        float totalChance = 0f;

        foreach(List<ObstacleData> odl in list) {
            foreach (ObstacleData od in odl) {
                totalChance += od.chance;
            }
        }
        return totalChance;
    }

    private ObstacleData ChooseWeightedObstacleData(params List<ObstacleData>[] obstacleDataArray) {
        if (obstacleDataArray == null || obstacleDataArray.Length == 0) return null;

        float totalChance = GetTotalChance(obstacleDataArray);
        float currentChance = 0f;
        float randomNumber = Random.Range(0f, totalChance);
        foreach (List<ObstacleData> odl in obstacleDataArray) {
            foreach (ObstacleData od in odl) {
                currentChance += od.chance;
                if (randomNumber <= currentChance) {
                    return od;
                }
            }
            
        }

        int n = obstacleDataArray.Length - 1;
        List<ObstacleData> last = obstacleDataArray[n];
        
        return last[last.Count - 1];
    }


}
