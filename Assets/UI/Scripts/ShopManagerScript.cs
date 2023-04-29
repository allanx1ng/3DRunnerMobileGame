using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManagerScript : MonoBehaviour
{
    public int coins;

    public TMP_Text coinUI;
    public List<Weapon> weapons;
    public GameObject ShopWeaponTemplate;

    public enum ShopMode
    {
        coins,
        weapons,
        skins
    }

    public ShopMode mode;

    private Initialize parentComponent;


    void Start()
    {

        parentComponent = GetComponentInParent<Initialize>();

        mode = ShopMode.weapons;
        UpdateShop();
    }

    void UpdateShop()
    {
        coins = parentComponent.getCoins();

        coinUI.text = "Coins " + coins.ToString();
        loadPanels();
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
        mode = ShopMode.weapons;
        loadPanels();

    }

    public void addCoins()
    {
        parentComponent.addCoins(1);
        UpdateShop();
    }

    void setAllTemplatesFalse()
    {
        Transform parentObject = ShopWeaponTemplate.transform.parent;
        Debug.Log(parentComponent.name);
        for (int i = 0; i < parentObject.transform.childCount; i++)
        {
            Transform child2 = parentObject.transform.GetChild(i);
            if (child2.name.Contains("Item Template(Clone)"))
            {
                Destroy(child2.gameObject);
            }
        }
    }

    public void purchaseItem(int i, int price)
    {

        switch (mode)
        {
            case ShopMode.coins:
                //
                break;
            case ShopMode.skins:
                //
                break;
            default:
                if (coins >= price)
                {
                    parentComponent.addCoins(-price);
                    weapons[i].isOwned = true;
                    UpdateShop();
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
                gameObject.transform.Find("ScrollRectWeapons").gameObject.SetActive(false);
                gameObject.transform.Find("ScrollRectSkins").gameObject.SetActive(false);
                gameObject.transform.Find("ScrollRectCoins").gameObject.SetActive(true);


                //
                break;
            case ShopMode.skins:
                gameObject.transform.Find("ScrollRectWeapons").gameObject.SetActive(false);
                gameObject.transform.Find("ScrollRectSkins").gameObject.SetActive(true);
                gameObject.transform.Find("ScrollRectCoins").gameObject.SetActive(false);
                //
                break;
            default:

                gameObject.transform.Find("ScrollRectWeapons").gameObject.SetActive(true);
                gameObject.transform.Find("ScrollRectSkins").gameObject.SetActive(false);
                gameObject.transform.Find("ScrollRectCoins").gameObject.SetActive(false);


                for (int i = 0; i < weapons.Count; i++)
                {
                    if (!weapons[i].isOwned)
                    {
                        GameObject duplicatedElement = Instantiate(ShopWeaponTemplate, ShopWeaponTemplate.transform.parent);
                        duplicatedElement.transform.SetSiblingIndex(ShopWeaponTemplate.transform.GetSiblingIndex() + 1); // set the position of the duplicated element to be right after the original element
                        duplicatedElement.SetActive(true);
                        Transform Panel = duplicatedElement.transform;
                        Panel.Find("Title").GetComponent<TMP_Text>().text = weapons[i].itemName;
                        Panel.Find("Description").GetComponent<TMP_Text>().text =
                        weapons[i].description + "\n\n" + "Damage to mobs: " + weapons[i].damageToMobs + "\n\n" + "Damage to blocks: " + weapons[i].damageToBlocks;
                        Panel.Find("Price").GetComponent<TMP_Text>().text = weapons[i].baseCost.ToString();
                        int index = i;
                        int price = weapons[i].baseCost;
                        Panel.Find("Purchase").GetComponent<Button>().onClick.AddListener(() => purchaseItem(index, price));
                    }

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
