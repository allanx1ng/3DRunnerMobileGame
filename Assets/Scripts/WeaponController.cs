using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject weaponHolder;
    [SerializeField] private GameObject projectileHolder;
    [SerializeField] private WeaponData weaponData;

    private List<GameObject> projectiles = new List<GameObject>();
    float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        EquipWeapon();
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
        Vector3 playerPosition = transform.position;

        projectile.transform.parent = weaponHolder.transform;
        projectile.transform.position = playerPosition;
        projectile.transform.eulerAngles = new Vector3(90f, 0f, 0f);

        projectiles.Add(projectile);
    }

    void EquipWeapon() {

        GameObject weaponModel = Instantiate(weaponData.weaponModel);
        weaponModel.transform.parent = weaponHolder.transform;
        weaponModel.transform.localPosition = new Vector3(0f, 0f, 0f);

        // TODO: Maybe add localPosition and localEulerAngles property in WeaponData if different weapons behave differently in the hand
        weaponModel.transform.localEulerAngles = new Vector3(90f, 0f, 0f);

    }
}
