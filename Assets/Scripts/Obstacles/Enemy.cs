using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public WeaponData weaponData;
    [SerializeField] private Material redOverlayMaterial;
    private float pulseDuration = 0.15f;
    private Renderer characterRenderer;
    private Material[] originalMaterials;

    void Start()
    {
        characterRenderer = transform.GetChild(0).GetComponent<Renderer>();
        originalMaterials = characterRenderer.materials;
        if (weaponData) StartCoroutine(SpawnProjectile());
    }
    public IEnumerator ApplyRedPulse() {
        Material[] redMaterials = new Material[originalMaterials.Length];
        for (int i = 0; i < redMaterials.Length; i++)
        {
            redMaterials[i] = redOverlayMaterial;
        }
        characterRenderer.materials = redMaterials;
        
        yield return new WaitForSeconds(pulseDuration);
        characterRenderer.materials = originalMaterials;
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
