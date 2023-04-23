using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public WeaponData weaponData;
    // [SerializeField] private bool isMelee = false;

    void Start()
    {
        gameObject.tag = "Mob"; // set to enemy
        if (weaponData) StartCoroutine(SpawnProjectile());
    }

    IEnumerator SpawnProjectile()
    {
        while (true)
        {

            GameObject projectilePrefab = weaponData.projectileModel;
            Quaternion rotation = Quaternion.Euler(weaponData.projectileRotation);

            GameObject spawnedProjectile = Instantiate(projectilePrefab, transform.position + weaponData.projectilePosition, rotation);
            EnemyProjectile enemyProjectileScript = spawnedProjectile.AddComponent<EnemyProjectile>();
            enemyProjectileScript.Initialize(weaponData);

            // spawnedProjectile.transform.parent = transform;
            yield return new WaitForSeconds(weaponData.delayBetweenShots);

        }
    }

    void OnTriggerEnter(Collider other) {
        GameObject playerObject = ObjectHelper.FindAncestorWithTag(other.gameObject, "Player");

        if (playerObject != null && playerObject.CompareTag("Player")) {
            Debug.Log("Enemy hit Player");
            PlayerController playerController = playerObject.GetComponent<PlayerController>();
            if (playerController) playerController.TakeDamage(1);
        }
    }
}
