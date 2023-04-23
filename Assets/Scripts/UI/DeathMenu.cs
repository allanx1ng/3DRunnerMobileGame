using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{

    public void ToggleDeathMenu(bool state) {
        Time.timeScale = 0f;
        gameObject.SetActive(state);
    }

    public void Restart() {
        Debug.Log("TODO: RESTART");
    }

    public void MainMenu() {

        gameObject.SetActive(false);
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1f;
    }
}
