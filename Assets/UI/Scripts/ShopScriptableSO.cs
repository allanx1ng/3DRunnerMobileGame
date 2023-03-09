using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(menuName = "ScriptableObjects/New Shop Item", order = 1)]
public class ShopScriptableSO : ScriptableObject
{
    public string title;
    public string description;
    public int baseCost;
}
