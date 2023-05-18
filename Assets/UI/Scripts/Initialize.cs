using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Initialize : MonoBehaviour
{

    // All UIs under canvas
    public GameObject[] UIList;

    public int coins;
    public int[] items;

    public Weapon[] weapons;

    private PlayerData data;

    private string path = "";
    private string persistentPath = "";
    
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
        CreatePlayerData();
        SetPaths();
        LoadData();
    }

    void Update() {

    }

    private void SetPaths()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "CoinData.json";
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "CoinData.json";
    }

    private void CreatePlayerData() {
        int[] temp = {0};
        data = new PlayerData(0,0,temp);
    }

    public void SaveData()
    {
        data.coins = getCoins();
        data.items = getItems();
        string savePath = path;

        Debug.Log("Saving Data at " + savePath);
        string json = JsonUtility.ToJson(data);
        Debug.Log(json);

        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);
        writer.Close();
    }

    public void LoadData()
    {
        using StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();

        data = JsonUtility.FromJson<PlayerData>(json);
        Debug.Log(data.ToString());
        coins = data.getCoins();
        items = data.getItems();
        Debug.Log(items);

        for(int i = 0; i<weapons.Length; i++) {
            weapons[i].isOwned = false; 
        }

        for (int i = 0; i<items.Length; i++) {
            int j = items[i];
            Weapon w = weapons[j];
            w.isOwned = true;
        }
    }
    public int getCoins() {
        return coins;
    }

    public void addCoins(int i) {
        coins += i;
        SaveData();
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
