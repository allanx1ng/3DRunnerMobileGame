using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public WeaponData weaponData;
    [SerializeField] private Material redPulseMaterial;
    private float pulseDuration = 0.5f;
    private Renderer characterRenderer;
    private Material originalMaterial;

    void Start()
    {
        characterRenderer = transform.GetChild(0).GetComponent<Renderer>();
        originalMaterial = characterRenderer.material;
        if (weaponData) StartCoroutine(SpawnProjectile());
    }
    public IEnumerator ApplyRedPulse() {
        characterRenderer.material = redPulseMaterial;
        yield return new WaitForSeconds(pulseDuration);
        characterRenderer.material = originalMaterial;
    }

    public void HandleProjectileHit(Projectile projectile, PlayerController playerController) {
        StartCoroutine(ApplyRedPulse());
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

    void OnTriggerEnter(Collider otherCollider) {
        PlayerManager.Instance.DamagePlayerIfHit(otherCollider);
    }
}
