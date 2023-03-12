using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    
    // The size of the block should include the MARGIN that a block should have between other blocks
    public float sizeOfBlock = 5f;
    private int countOfGeneratedRows = 0;

    private List<GameObject> obstacleRows = new List<GameObject>();
    [SerializeField] private GameObject obstacleHolder;
    private void GenerateBlock(Vector3 position, GameObject obstaclePrefab, GameObject obstacleRow) {
        // Generate an obstacle at the given position
        
        GameObject obstacle = Instantiate(obstaclePrefab, position, Quaternion.identity);
        obstacle.transform.SetParent(obstacleRow.transform);
    }

    public void GenerateMob() {

    }

    public void GenerateObstacles(Vector3 position, int amount, TerrainData terrainData) {
        // Generate multiple obstacles centered at the given position, creating a row of obstacles on the x-axis

        float x = position.x;
        float currentX = 0 - (((float) amount - 1) / 2 * sizeOfBlock);    

        GameObject obstacleRow = new GameObject("Obstacle Row " + countOfGeneratedRows);
        obstacleRow.transform.SetPositionAndRotation(position, Quaternion.identity);
        obstacleRow.transform.SetParent(obstacleHolder.transform);
        obstacleRows.Add(obstacleRow);
         
        for (int i = 0; i < amount; i++) {

            GameObject block = ChooseBlock(terrainData);
            GenerateBlock(new Vector3(currentX, position.y + 3, position.z), block, obstacleRow);
            currentX += sizeOfBlock;
        }
        
        countOfGeneratedRows++;
    }

    public GameObject ChooseBlock(TerrainData terrainData) {
        List<GameObject> blockList = terrainData.spawnableBlocks;
        return blockList[Random.Range(0, blockList.Count)];
    }

    
    public void DeleteObstacleRow() {
        if (obstacleRows.Count < 1) return;

        Destroy(obstacleRows[0]);
        obstacleRows.RemoveAt(0);


    }


}
