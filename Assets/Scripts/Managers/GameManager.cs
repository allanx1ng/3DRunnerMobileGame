using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;
    private PlayerData playerData;

    public string path = "";
    public string persistentPath = "";
    

    public void Awake() {
        if (Instance == null) {
            Instance = this;
            gameObject.transform.parent = null; // only top level can use dont destroy on load
            Init();
        } else {
            Debug.LogError("More than one GameManager in the scene, destroying duplicate");
            Destroy(gameObject);
            
        }
    }

    public int getCoins() {
        return playerData.getCoins();
    }
    
    public void addCoins(int i) {
        playerData.setCoins(getCoins() + i);
        playerData.SaveData(path);
    }

    public void SetWeapon(int i) {
        playerData.setWeaponEquipped(i);
        playerData.SaveData(path);
    }

    private void SetPaths()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "CoinData.json";
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "CoinData.json";
    }

    private void Init() {
        SetPaths();
        playerData = new PlayerData(path);
    }

    public void RestartGame()
    {
        // Get the index of the current active scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the current scene using its build index
        SceneManager.LoadScene(currentSceneIndex);
        Time.timeScale = 1f;
    }

    public PlayerData GetPlayerData() {
        return playerData;
    }
    



}
