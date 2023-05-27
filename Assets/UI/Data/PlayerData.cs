using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class PlayerData
{
    public int coins;
    public int[] items;

    public int weaponEquipped;
    public int highScore;

    public PlayerData(string path) {
        LoadData(path);
    }
    public PlayerData(int coins, int[] items)
    {
        this.coins = coins;
        this.items = items;
    }

    public void SaveData(string path)
    {
        Debug.Log("Saving Data at " + path);
        string json = JsonUtility.ToJson(this);
        Debug.Log(json);

        using StreamWriter writer = new StreamWriter(path);
        writer.Write(json);
        writer.Close();
    }

    public void LoadData(string path)
    {
        using StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();

        PlayerData data = JsonUtility.FromJson<PlayerData>(json);
        Debug.Log(data.ToString());
        coins = data.getCoins();
        items = data.getItems();
        weaponEquipped = data.getWeaponEquipped();
        highScore = data.getHighScore();

    }

    public override string ToString()
    {
        return $"Player has {coins} coins";
    }

    public int getCoins() {
        return coins;
    }

    public void setCoins(int i) {
        this.coins = i;
    }

    public int getWeaponEquipped() { 
        return weaponEquipped;
    }

    public void setWeaponEquipped(int weapon) {
        weaponEquipped = weapon;
    }


    public int getHighScore() {
        return highScore;
    }

    public void setHighScore(int score) {
        highScore = score;
    }

    public int[] getItems() {
        return items;
    }
}
