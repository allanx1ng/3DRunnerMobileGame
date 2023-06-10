using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject weaponHolder;
    [SerializeField] private WeaponData weaponData;
    public GameManager gameManager;
    private GameObject projectileHolder;
    private GameObject currentWeaponModel;
    private List<GameObject> projectiles = new List<GameObject>();
    float timer = 0f;

    void Awake() {
        SetGameManager();
    }

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

    private void OnEnable()
    {
        gameManager.onWeaponChanged.AddListener(HandleWeaponChanged);
    }

    private void OnDisable()
    {
        gameManager.onWeaponChanged.RemoveListener(HandleWeaponChanged);
    }

    private void HandleWeaponChanged()
    {
        // The value has changed, do something here
        EquipWeapon(GameManager.Instance.GetWeapon());
    }

    private void SetGameManager() {
        // Find the GameManager object in the scene
        GameObject gmObject = GameObject.Find("GameManager");

        // Check if the GameManager object was found
        if (gmObject != null)
        {
            // Get the GameManager component
            gameManager = gmObject.GetComponent<GameManager>();
        }
        else
        {
            Debug.LogError("GameManager object not found in scene");
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

        if (currentWeaponModel != null) {
            Destroy(currentWeaponModel);
        }

        weaponData = WeaponManager.Instance.GetWeapon(itemId);

        currentWeaponModel = Instantiate(weaponData.weaponModel);
        currentWeaponModel.transform.parent = weaponHolder.transform;
        currentWeaponModel.transform.localPosition = weaponData.weaponPosition;

        // TODO: Maybe add localPosition and localEulerAngles property in WeaponData if different weapons behave differently in the hand
        currentWeaponModel.transform.localEulerAngles = weaponData.weaponRotation;

    }
}
