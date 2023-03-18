using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public int coins;
    public int gems;
    private Initialize parentComponent;
    public TMP_Text coinUI;
    public TMP_Text gemUI;
    public GameObject powerupItemTemplate;
    public GameObject skinItemTemplate;
    public Item[] skins;
    public Item[] powerups;

    void Start()
    {
        parentComponent = GetComponentInParent<Initialize>();
        loadPanels();

    }

    void Update()
    {
        coins = parentComponent.getCoins();
        coinUI.text = "Coins " + coins.ToString();

        // gems = parentComponent.getGems();
        // gemUI.text = "Coins " + gems.ToString();
    }

    public void closeInventory()
    {
        gameObject.transform.parent.Find("Panel").gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void loadPanels()
    {
        for (int i = 0; i < powerups.Length; i++)
        {
            GameObject duplicatedElement = Instantiate(powerupItemTemplate, powerupItemTemplate.transform.parent);
            duplicatedElement.transform.SetSiblingIndex(powerupItemTemplate.transform.GetSiblingIndex() + 1); // set the position of the duplicated element to be right after the original element
            duplicatedElement.SetActive(true);
            Transform Panel = duplicatedElement.transform.Find("Panel");
            Panel.Find("Title").GetComponent<TMP_Text>().text = powerups[i].itemName;
        }
        for (int i = 0; i < skins.Length; i++)
        {
            DuplicateSkinElement();
        }

    }


    public void DuplicatePowerUpElement()
    {


    }

    public void DuplicateSkinElement()
    {
        GameObject duplicatedElement2 = Instantiate(skinItemTemplate, skinItemTemplate.transform.parent);
        duplicatedElement2.transform.SetSiblingIndex(skinItemTemplate.transform.GetSiblingIndex() + 1); // set the position of the duplicated element to be right after the original element
        duplicatedElement2.SetActive(true);
    }
}
