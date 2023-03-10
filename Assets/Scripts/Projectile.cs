using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    public WeaponData weaponData;
    public PlayerController playerController; // the player controller script

    float distanceTravelled = 0f;

    void Start() {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update() {
        UpdateTransforms();
        DestroyIfNeeded(); // destroy after else modifies destroyed properties
    }

    private void OnTriggerEnter(Collider other) {
        GameObject otherObject = other.gameObject;
        // Debug.Log("Hitting");
        if (otherObject.tag == "Block") {
            
            Destroy(otherObject);
        }

    }

    private void DestroyIfNeeded() {

        if (distanceTravelled >= weaponData.distanceUntilDestruction) {
            Destroy(gameObject);
        }

    }


    private void UpdateTransforms() {
        float playerSpeed = playerController.GetSpeed();

        float rot = Time.deltaTime * weaponData.rotationSpeed;
        transform.Rotate(Vector3.right, rot);

        float forwardDistance = playerSpeed + weaponData.speed;
        forwardDistance *= Time.deltaTime;
        Vector3 movement = new Vector3(0, 0, forwardDistance);
        transform.Translate(movement, Space.World);

        distanceTravelled += forwardDistance;
    }

}

