using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;

    public void Awake() {
        if (Instance == null) {
            Instance = this;
            gameObject.transform.parent = null; // only top level can use dont destroy on load

        } else {
            Debug.LogError("More than one GameManager in the scene, destroying duplicate");
            Destroy(gameObject);
            
        }
    }

    public void RestartGame()
    {
        // Get the index of the current active scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the current scene using its build index
        SceneManager.LoadScene(currentSceneIndex);
        Time.timeScale = 1f;
    }

}
