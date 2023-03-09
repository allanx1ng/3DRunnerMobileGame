using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManagerScript : MonoBehaviour
{
    public int coins;
    
    public TMP_Text coinUI;
    public ShopScriptableSO[] skins;
    public ShopScriptableSO[] powerups;
    public ShopScriptableSO[] purchaseCoins;

    public ShopTemplate[] shopPanels;
    public GameObject[] shopPanelsSO;
    public Button[] myPurchaseBtns;

    public enum ShopMode
    {
        coins,
        powerups,
        skins
    }

    public ShopMode mode;

    private Initialize parentComponent;


    void Start()
    {

        parentComponent = GetComponentInParent<Initialize>();
        
        mode = ShopMode.powerups;
        Update();
        loadPanels();
        checkPurchaseable();
    }

    void Update()
    {
        coins = parentComponent.getCoins();

        coinUI.text = "Coins " + coins.ToString();
    }

    public void switchToCoins()
    {
        mode = ShopMode.coins;
        loadPanels();

    }
    public void switchToSkins()
    {
        mode = ShopMode.skins;
        loadPanels();

    }
    public void switchToPowerUps()
    {
        mode = ShopMode.powerups;
        loadPanels();

    }

    public void addCoins()
    {
        parentComponent.addCoins(1);
        Update();
        checkPurchaseable();
    }

    void setAllTemplatesFalse()
    {
        for (int i = 0; i < shopPanelsSO.Length; i++)
            shopPanelsSO[i].SetActive(false);
    }

    public void checkPurchaseable()
    {


        switch (mode)
        {
            case ShopMode.coins:
                for (int i = 0; i < purchaseCoins.Length; i++)
                {
                    if (coins >= purchaseCoins[i].baseCost)
                    {
                        myPurchaseBtns[i].interactable = true;
                    }
                    else
                    {
                        myPurchaseBtns[i].interactable = false;
                    }


                }
                break;
            case ShopMode.skins:
                for (int i = 0; i < skins.Length; i++)
                {
                    if (coins >= skins[i].baseCost)
                    {
                        myPurchaseBtns[i].interactable = true;
                    }
                    else
                    {
                        myPurchaseBtns[i].interactable = false;
                    }


                }
                break;
            default:
                for (int i = 0; i < powerups.Length; i++)
                {
                    if (coins >= powerups[i].baseCost)
                    {
                        myPurchaseBtns[i].interactable = true;
                    }
                    else
                    {
                        myPurchaseBtns[i].interactable = false;
                    }


                }
                break;
        }
    }

    public void purchaseItem(int btnIndex)
    {

        switch (mode)
        {
            case ShopMode.coins:
                if (coins >= purchaseCoins[btnIndex].baseCost)
                {
                    parentComponent.addCoins(-purchaseCoins[btnIndex].baseCost);
                    Update();
                    checkPurchaseable();
                    //unlock item 
                }
                break;
            case ShopMode.skins:
                if (coins >= skins[btnIndex].baseCost)
                {
                    parentComponent.addCoins(-skins[btnIndex].baseCost);
                    Update();
                    checkPurchaseable();
                    //unlock item 
                }
                break;
            default:
                if (coins >= powerups[btnIndex].baseCost)
                {
                     parentComponent.addCoins(-powerups[btnIndex].baseCost);
                    Update();
                    checkPurchaseable();
                    //unlock item 
                }
                break;
        }
    }

    public void loadPanels()
    {
        setAllTemplatesFalse();
        Debug.Log("Hello, world!");


        switch (mode)
        {
            case ShopMode.coins:

                for (int i = 0; i < purchaseCoins.Length; i++)
                    shopPanelsSO[i].SetActive(true);

                for (int i = 0; i < purchaseCoins.Length; i++)
                {
                    shopPanels[i].titleTxt.text = purchaseCoins[i].title;
                    shopPanels[i].descriptionTxt.text = purchaseCoins[i].description;
                    shopPanels[i].costTxt.text = "Coins " + purchaseCoins[i].baseCost.ToString();
                }
                break;
            case ShopMode.skins:

                for (int i = 0; i < skins.Length; i++)
                    shopPanelsSO[i].SetActive(true);

                for (int i = 0; i < skins.Length; i++)
                {
                    shopPanels[i].titleTxt.text = skins[i].title;
                    shopPanels[i].descriptionTxt.text = skins[i].description;
                    shopPanels[i].costTxt.text = "Coins " + skins[i].baseCost.ToString();
                }
                break;
            default:

                for (int i = 0; i < powerups.Length; i++)
                    shopPanelsSO[i].SetActive(true);


                for (int i = 0; i < powerups.Length; i++)
                {
                    shopPanels[i].titleTxt.text = powerups[i].title;
                    shopPanels[i].descriptionTxt.text = powerups[i].description;
                    shopPanels[i].costTxt.text = "Coins " + powerups[i].baseCost.ToString();
                }
                break;
        }

    }

    public void closeShop()
    {
        switchToPowerUps();
        gameObject.transform.parent.Find("Panel").gameObject.SetActive(true);
        gameObject.SetActive(false);
    }


}
