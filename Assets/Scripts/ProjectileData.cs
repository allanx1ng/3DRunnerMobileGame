using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile Data", menuName = "Projectile Data")]
public class ProjectileData : ScriptableObject
{
    
    public float speed;
    public float damage;
    public float multiplierToMobs;
    public float multiplierToBlocks;
    public float rotationPerSecond;

}
