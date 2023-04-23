using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public int coins;
    public int gems;
    public int[] items;

    public PlayerData(int coins, int gems, int[] items)
    {
        this.gems = gems;
        this.coins = coins;
        this.items = items;
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

    public int[] getItems() {
        return items;
    }
}
