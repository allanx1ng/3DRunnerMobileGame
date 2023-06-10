using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Weapon Data", menuName = "Weapon Data")]
public class WeaponData : ScriptableObject
{
    
    // Model Connections
    [Header("Model Prefabs")]
    public GameObject weaponModel;
    public GameObject projectileModel;
    public int itemId;  // your weapon properties


    [Header("Transform Options")]
    // Model Offsets
    public Vector3 weaponPosition;
    public Vector3 weaponRotation;
    public Vector3 projectilePosition;
    public Vector3 projectileRotation;


    [Header("Main Stats (Shared Enemy & Player)")]
    // Projectile Stats
    public float damage = 1f;
    public float speed = 10f;
    public float delayBetweenShots = 0.25f;    // the firing rate of the weapon, 2f = 2 seconds between each shot
    public float distanceUntilDestruction = 100f;
    public float coinsPerHit = 1f;


    [Header("Misc")]
    public Vector3 rotationVector = new Vector3(0, 0, 0);
    public float incomeMultiplier = 1f;
    public float multiplierToMobs = 1f;
    public float multiplierToBlocks = 1f;


}
