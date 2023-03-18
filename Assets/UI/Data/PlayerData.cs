using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public int coins;
    public int gems;
    public int[] items;

    public PlayerData(int coins, int gems)
    {
        this.gems = gems;
        this.coins = coins;
    }

    public override string ToString()
    {
        return $"Player has {coins} coins and {gems} gems";
    }

    public int getCoins() {
        return coins;
    }

    public void setCoins(int i) {
        this.coins = i;
    }
}
