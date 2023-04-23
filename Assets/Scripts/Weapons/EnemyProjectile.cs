using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private WeaponData weaponData;
    float distanceTravelled = 0f;

    void Start() {
        gameObject.tag = "Enemy Projectile";
    }

    public void Initialize(WeaponData weaponData) {
        this.weaponData = weaponData;
    }

    private void Update() {
        UpdateTransforms();
        DestroyIfNeeded(); // destroy after else modifies destroyed properties
    }

    private void DestroyIfNeeded() {

        if (distanceTravelled >= weaponData.distanceUntilDestruction) {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider otherCollider) {
        PlayerManager.Instance.DamagePlayerIfHit(otherCollider);

    }
    private void UpdateTransforms() {

        Vector3 forward = new Vector3(0, 0, -1f);
        float forwardSpeed = weaponData.speed;
        forward *= forwardSpeed * Time.deltaTime;
        transform.Translate(forward, Space.World);
        distanceTravelled += forward.magnitude;
        
    }

    public WeaponData GetWeaponData() {
        return weaponData;
    }
}
