using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject weaponHolder;
    [SerializeField] private WeaponData weaponData;
    private GameObject projectileHolder;
    private List<GameObject> projectiles = new List<GameObject>();
    float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        FindAndSetProjectileHolder();
        EquipWeapon(GameManager.Instance.GetWeapon());
    }

    void FindAndSetProjectileHolder() {
        projectileHolder = GameObject.Find("Projectile Holder");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= weaponData.delayBetweenShots) {
            ShootProjectile();
            timer -= weaponData.delayBetweenShots;
        }
        
    }

    void ShootProjectile() {
        GameObject projectile = Instantiate(weaponData.projectileModel);
        Projectile projectileScript = projectile.AddComponent<Projectile>();
        projectileScript.Initialize(weaponData);
        Vector3 playerPosition = transform.position;
        playerPosition.y += 3f; // since the transform.position is at the character's feet, we need to offset it.

        projectile.transform.parent = projectileHolder.transform;
        projectile.transform.position = playerPosition + weaponData.projectilePosition;
        projectile.transform.localEulerAngles = weaponData.projectileRotation;

        projectiles.Add(projectile);
    }

    void EquipWeapon(int itemId) {

        weaponData = WeaponManager.Instance.GetWeapon(itemId);

        GameObject weaponModel = Instantiate(weaponData.weaponModel);
        weaponModel.transform.parent = weaponHolder.transform;
        weaponModel.transform.localPosition = weaponData.weaponPosition;

        // TODO: Maybe add localPosition and localEulerAngles property in WeaponData if different weapons behave differently in the hand
        weaponModel.transform.localEulerAngles = weaponData.weaponRotation;

    }
}
