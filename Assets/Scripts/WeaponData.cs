using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Weapon Data", menuName = "Weapon Data")]
public class WeaponData : ScriptableObject
{
    
    // Model Connections
    public GameObject weaponModel;
    public GameObject projectileModel;


    // Projectile Stats
    public float damage = 1f;
    public float speed = 3f;
    public float delayBetweenShots = 2f;    // the firing rate of the weapon, 2f = 2 seconds between each shot
    public float distanceUntilDestruction = 10f;
    public float rotationSpeed = 90f;
    public float incomeMultiplier = 1f;
    public float multiplierToMobs = 1f;
    public float multiplierToBlocks = 1f;


    

}
