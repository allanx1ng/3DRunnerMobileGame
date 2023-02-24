using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManagerScript : MonoBehaviour
{
    public int coins;
    public TMP_Text coinUI;
    public ShopScriptableSO[] shopItemSO;
    public ShopTemplate[] shopPanels;
    public GameObject[] shopPanelsSO;
    public Button[] myPurchaseBtns;

    void Start()
    {
        for (int i = 0; i < shopItemSO.Length; i++)
            shopPanelsSO[i].SetActive(true);
            Update();
        loadPanels();
        checkPurchaseable();
    }

    void Update()
    {
        coinUI.text = "Coins " + coins.ToString();
    }

    public void addCoins()
    {
        coins++;
        Update();
        checkPurchaseable();
    }

    public void checkPurchaseable()
    {
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            if (coins >= shopItemSO[i].baseCost)
            {
                myPurchaseBtns[i].interactable = true;
            }
            else
            {
                myPurchaseBtns[i].interactable = false;
            }


        }
    }

    public void purchaseItem(int btnIndex) {
        if(coins >= shopItemSO[btnIndex].baseCost) {
            coins -= shopItemSO[btnIndex].baseCost;
            Update(); 
            checkPurchaseable();
            //unlock item 
        }
    }

    public void loadPanels()
    {
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            shopPanels[i].titleTxt.text = shopItemSO[i].title;
            shopPanels[i].descriptionTxt.text = shopItemSO[i].description;
            shopPanels[i].costTxt.text = "Coins " + shopItemSO[i].baseCost.ToString();
        }
    }

    public void closeShop() {
        gameObject.transform.parent.Find("Panel").gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void openShop() {
        gameObject.transform.parent.Find("Panel").gameObject.SetActive(false);
        gameObject.SetActive(true);

    }
}
