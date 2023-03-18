using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/New Item", order = 1)]
public class Item : ScriptableObject
{
    public string itemName;
    public int itemID;
    public string description;
    public bool isBuyable;
    public int baseCost;
    public bool isOwned;
    public bool isEquipped;
}
