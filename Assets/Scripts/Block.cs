using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    [SerializeField] private float maxHealth = 7;
    private float currentHealth = 0;
    [SerializeField] List<GameObject> stageModels = new List<GameObject>();
    [SerializeField] private GameObject effect;
    private GameObject breakingStageBlock = null;
    private int currentStage = -1; // stage of -1 means that it is not using any stageModels game objet right now

    void Start() {
        currentHealth = maxHealth;
    }

    public void HandleProjectileHit(Projectile projectileScript) {

        WeaponData weaponData = projectileScript.GetWeaponData();

        currentHealth -= weaponData.damage;

        if (currentHealth <= 0) {

            Instantiate(effect, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }
        float modelCount = stageModels.Count;
        int nextStage = (int) (modelCount - (modelCount * (currentHealth / maxHealth)));
        nextStage = nextStage >= modelCount ? (int) modelCount - 1 : nextStage;
        
        if (nextStage != currentStage) {
            if (breakingStageBlock != null) Destroy(breakingStageBlock);
            
            GameObject breakingModel = Instantiate(stageModels[nextStage]);

            breakingModel.transform.parent = gameObject.transform;
            breakingModel.transform.localPosition = Vector3.zero;
            breakingModel.transform.localEulerAngles = Vector3.zero;

            breakingStageBlock = breakingModel;
        }





    }


}
