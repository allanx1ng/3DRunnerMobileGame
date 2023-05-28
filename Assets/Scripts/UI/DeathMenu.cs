using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Add this line if using TextMeshPro

public class DeathMenu : MonoBehaviour
{
    public TextMeshProUGUI scoreText; 
    public TextMeshProUGUI highScoreText;
    public void ToggleDeathMenu(bool state) {
        Time.timeScale = 0f;
        gameObject.SetActive(state);

        if (state) {
            // player died.
            int coins = CoinManager.Instance.GetCoins();
            GameManager.Instance.addCoins(coins);

            if (GameManager.Instance.SetHighScoreIfBeaten(coins)) {
                Debug.Log("High score beaten!");
            }

            highScoreText.text = "High Score: " + GameManager.Instance.GetHighScore();
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
