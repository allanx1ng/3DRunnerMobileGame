using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Add this line if using TextMeshPro

public class DeathMenu : MonoBehaviour
{
    public TextMeshProUGUI scoreText; 
    public void ToggleDeathMenu(bool state) {
        Time.timeScale = 0f;
        gameObject.SetActive(state);

        if (state) {
            // player died.
            int coins = CoinManager.Instance.GetCoins();
            GameManager.Instance.addCoins(coins);
            scoreText.text = "Score: " + coins;
        }
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
