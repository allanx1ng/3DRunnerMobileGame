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

    public GameObject InGameUI;
    public GameObject MenuUI;
    

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

    public int GetHighScore() {
        return playerData.getHighScore();
    }

    // true if high score is beaten, false if not
    public bool SetHighScoreIfBeaten(int score) {
        int s = playerData.getHighScore();
        if (score > s) {
            playerData.setHighScore(score);
            playerData.SaveData(path);
            return true;
        }
        return false;
    }

    public int GetWeapon() {
        return playerData.getWeaponEquipped();
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
        PlayerPrefs.SetInt("ShouldStartGame", 1);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the current scene using its build index
        SceneManager.LoadScene(currentSceneIndex);
        // Time.timeScale = 1f;
        // InGameUI.SetActive(true);
        // MenuUI.SetActive(false);
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("ShouldStartGame", 0);
        InGameUI.SetActive(true);
        MenuUI.SetActive(false);
         Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        // Get the index of the current active scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the current scene using its build index
        SceneManager.LoadScene(currentSceneIndex);
        // Time.timeScale = 0f;
        // InGameUI.SetActive(false);
        // MenuUI.SetActive(true);
    }

    public PlayerData GetPlayerData() {
        return playerData;
    }
    



}
