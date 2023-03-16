using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelSwitcher : MonoBehaviour
{
    public GameObject[] playerModels; // An array of player model prefabs
    private GameObject currentModel; // The current player model
    
    private void Start()
    {
        // Load the initial player model
        LoadPlayerModel(0);
    }

    public void LoadPlayerModel(int index)
    {
        // Destroy the current player model
        if (currentModel != null)
            Destroy(currentModel);

        // Instantiate the new player model
        currentModel = Instantiate(playerModels[index], transform);

        // Set the parent of the new player model to this holder object
        currentModel.transform.SetParent(transform);

    }

}
