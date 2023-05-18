using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/New Weapon", order = 1)]
public class Weapon : ScriptableObject
{
    public string itemName;
    public int itemID;
    public string description;
    public int baseCost;
    public bool isOwned;
    public bool isEquipped;

    public int damageToMobs;
    public int damageToBlocks;
    public Sprite icon;
    

}
