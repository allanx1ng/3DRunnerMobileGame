using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Add this line if using TextMeshPro
public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance = null;

    private int coins = 0;
    public TextMeshProUGUI coinText; 

    public void Awake() {
        if (Instance == null) {
            Instance = this;
            // Probably do something about setting the coins from the saved json file here
            
        } else {
            Debug.LogError("Multiple instances of UIManager detected. Destroying the duplicate.");
            Destroy(gameObject);
        }
    }

    public void AddCoins(int coins) {
        this.coins += coins;
        UpdateCoinUI();
    }

    void UpdateCoinUI()
    {
        coinText.text = coins.ToString();
    }


}
