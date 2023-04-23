using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused;

    public void Pause() {
        gameObject.SetActive(true);
        gameObject.transform.parent.Find("Pause Button").gameObject.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume() {

        gameObject.SetActive(false);
        gameObject.transform.parent.Find("Pause Button").gameObject.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Restart() {
        gameObject.SetActive(false);
        gameObject.transform.parent.Find("Panel").gameObject.SetActive(true);
    }

    public void MainMenu() {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1f;
    }
}
