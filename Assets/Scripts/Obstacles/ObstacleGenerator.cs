using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    
    // The size of the block should include the MARGIN that a block should have between other blocks
    public float sizeOfBlock = 5f;
    public float chanceForNothing = 85f;
    public float chanceForMob = 10f;
    public float chanceForBlock = 5f;

    [SerializeField] private List<ObstacleData> commonBlockData = new List<ObstacleData>(); // blocks that are spawnable in all biomes
    [SerializeField] private List<ObstacleData> commonMobData = new List<ObstacleData>();
    private List<GameObject> obstacleRows = new List<GameObject>();
    [SerializeField] private GameObject obstacleHolder;
    private int countOfGeneratedRows = 0;
    private void GenerateObstacle(Vector3 position, GameObject obstaclePrefab, GameObject obstacleRow) {
        // Generate an obstacle at the given position
        
        GameObject obstacle = Instantiate(obstaclePrefab, position, Quaternion.identity);
        obstacle.transform.SetParent(obstacleRow.transform);
    }



    public void GenerateObstacles(Vector3 position, int amount, TerrainData terrainData) {
        
        // Generate multiple obstacles centered at the given position, creating a row of obstacles on the x-axis
        float startX = 0 - (((float) amount - 1) / 2 * sizeOfBlock);    

        GameObject obstacleRow = new GameObject("Obstacle Row " + countOfGeneratedRows);
        obstacleRow.transform.SetPositionAndRotation(position, Quaternion.identity);
        obstacleRow.transform.SetParent(obstacleHolder.transform);
        obstacleRows.Add(obstacleRow);

        // Chance of spawning nothing vs. block vs. mob
        float totalChance = chanceForBlock + chanceForMob + chanceForNothing;

        // Dont generate obstacles for objects with no children (roads)
        if (terrainData.canSpawnBlocks) {
            for (int i = 0; i < amount; i++) {

                float randomNumber = Random.Range(0f, totalChance);
                float newX = startX + (i * sizeOfBlock);

                if (randomNumber <= chanceForNothing) {
                    // do nothing
                    continue;
                } else if (randomNumber <= chanceForMob + chanceForNothing) {
                    // spawn mob
                    GameObject mob = ChooseMob(terrainData);
                    GenerateObstacle(new Vector3(newX, position.y + 3, position.z), mob, obstacleRow);
                } else {
                    // spawn block
                    GameObject block = ChooseBlock(terrainData);
                    GenerateObstacle(new Vector3(newX, position.y + 3, position.z), block, obstacleRow);
                }
                
            }
        } 
        
        countOfGeneratedRows++;
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
