using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    public WeaponData weaponData;

    private void Start() {

    }    

    private void Update() {
        float rot = Time.deltaTime * weaponData.rotationSpeed;
        transform.Rotate(Vector3.right, rot);

        float forwardDistance = Time.deltaTime * weaponData.speed;
        Vector3 movement = new Vector3(0, 0, forwardDistance);
        transform.Translate(movement, Space.World);
    }

}

