using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Initialize : MonoBehaviour
{

    // All UIs under canvas
    public GameObject[] UIList;
    public Weapon[] weapons;
    private PlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {

        
        
        for (int i = 0; i<UIList.Length;i++) {
            if (UIList[i].name == "Main Menu") {
                UIList[i].SetActive(true);
            } else {
                UIList[i].SetActive(false);
            }
        }

        if (PlayerPrefs.GetInt("ShouldStartGame") == 1)
        {
            GameManager.Instance.StartGame();
        } else {
            Time.timeScale = 0f;
        }
        
        Init();
    }

    void Init() {
        playerData = GameManager.Instance.GetPlayerData();
        for(int i = 0; i<weapons.Length; i++) {
            weapons[i].isOwned = false; 
        }

        int[] items = playerData.getItems();
        
        for (int i = 0; i<items.Length; i++) {
            int j = items[i];
            Weapon w = weapons[j];
            w.isOwned = true;
        }
    }

    public void addCoins(int i) {
        GameManager.Instance.addCoins(i);
    }

    public int getCoins() {
        return GameManager.Instance.getCoins();
    }


    public int[] getItems() {
        List<int> tempList = new List<int>();
        for (int i = 0; i<weapons.Length; i++) {
            if (weapons[i].isOwned) {
                tempList.Add(weapons[i].itemID);
            }
        }
        int[] tempArr = tempList.ToArray();
        return tempArr;
    }



}
