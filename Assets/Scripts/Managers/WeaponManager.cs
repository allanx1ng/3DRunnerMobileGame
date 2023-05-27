using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // Singleton instance
    private static WeaponManager _instance;
    public static WeaponManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<WeaponManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("WeaponManager");
                    _instance = go.AddComponent<WeaponManager>();
                }
            }
            return _instance;
        }
    }

    // List of WeaponData
    public List<WeaponData> weapons;

    // Function to get a weapon by itemId
    public WeaponData GetWeapon(int itemId)
    {
        foreach (WeaponData weapon in weapons)
        {
            if (weapon.itemId == itemId)
            {
                return weapon;
            }
        }

        return null;
    }
}