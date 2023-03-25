using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    // Block properties
    [SerializeField] private float maxHealth = 7;
    private float currentHealth = 0;

    // Gem Emission
    [SerializeField] private GameObject gemParticleSystem;

    // Pulsation
    private float pulsateScale = 1.1f;
    private float pulsateSpeed = 0.05f;
    private bool pulsating = false;

    // Block breaking
    [SerializeField] private GameObject effect;
    [SerializeField] List<GameObject> stageModels = new List<GameObject>();
    private GameObject breakingStageBlock = null;
    private int currentStage = -1; // stage of -1 means that it is not using any stageModels game objet right now


    void Start() {
        currentHealth = maxHealth;
    }

    public void HandleProjectileHit(Projectile projectileScript) {

        WeaponData weaponData = projectileScript.GetWeaponData();

        TakeDamageFromProjectile(weaponData);
        AdvanceBreakStageIfApplicable();


        if (!pulsating) StartCoroutine(Pulsate());

    }

    private void TakeDamageFromProjectile(WeaponData weaponData) {
        currentHealth -= weaponData.damage;

        if (currentHealth <= 0) {

            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }
    }

    private void AdvanceBreakStageIfApplicable() {
        float modelCount = stageModels.Count;
        int nextStage = (int) (modelCount - (modelCount * (currentHealth / maxHealth)));
        nextStage = nextStage >= modelCount ? (int) modelCount - 1 : nextStage;
        
        if (nextStage != currentStage) {
            
            GameObject breakingModel = Instantiate(stageModels[nextStage]);

            breakingModel.transform.parent = gameObject.transform;
            breakingModel.transform.localPosition = Vector3.zero;
            breakingModel.transform.localEulerAngles = Vector3.zero;

            if (breakingStageBlock != null) Destroy(breakingStageBlock);

            breakingStageBlock = breakingModel;
        }
    }

    IEnumerator Pulsate()
    {
        if (pulsating) yield break;
        pulsating = true;

        // doesn't look the best to be honest, commenting out for now.
        Instantiate(gemParticleSystem, transform.position, Quaternion.identity);

        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = originalScale * pulsateScale;
        float t = 0;

        while (t < 1)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            t += Time.deltaTime / pulsateSpeed;
            yield return null;
        }

        t = 0;

        while (t < 1)
        {
            transform.localScale = Vector3.Lerp(targetScale, originalScale, t);
            t += Time.deltaTime / pulsateSpeed;
            yield return null;
        }
        transform.localScale = originalScale;
        if (breakingStageBlock != null) breakingStageBlock.transform.localScale = new Vector3(1f, 1f, 1f);
        pulsating = false;

    }



}
