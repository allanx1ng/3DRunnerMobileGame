using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public int coins;
    private Initialize parentComponent;
    public TMP_Text coinUI;
    public TMP_Text gemUI;
    public GameObject powerupItemTemplate;
    public GameObject skinItemTemplate;
    public Item[] skins;
    public Weapon[] weapons;

    void Start()
    {
        parentComponent = GetComponentInParent<Initialize>();
        // string path = Application.dataPath + "/Assets/Items/Weapons/";
        // DirectoryInfo directory = new DirectoryInfo(path);
        // FileInfo[] files = directory.GetFiles();
        // foreach (FileInfo file in files)
        // {
        //     Weapon item = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/MyFolder/" + file.Name);
        //     weapons.Add(item);
        // }

        loadPanels();
    }

    void Update()
    {
        coins = parentComponent.getCoins();
        coinUI.text = "Coins " + coins.ToString();
    }

    public void closeInventory()
    {
        closePanels();
        gameObject.transform.parent.Find("Panel").gameObject.SetActive(true);
        gameObject.SetActive(false);
        loadPanels();
        
    }

    public void openInventory() {
        gameObject.transform.parent.Find("Panel").gameObject.SetActive(false);
        gameObject.SetActive(true);
        loadPanels();
    }

    public void loadPanels()
    {
        DuplicatePowerUpElement();

        for (int i = 0; i < skins.Length; i++)
        {
            DuplicateSkinElement();
        }

    }


    public void DuplicatePowerUpElement()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].isOwned)
            {
                GameObject duplicatedElement = Instantiate(powerupItemTemplate, powerupItemTemplate.transform.parent);
                duplicatedElement.transform.SetSiblingIndex(powerupItemTemplate.transform.GetSiblingIndex() + 1); // set the position of the duplicated element to be right after the original element
                duplicatedElement.SetActive(true);
                Transform Panel = duplicatedElement.transform.Find("Panel");
                Panel.Find("Title").GetComponent<TMP_Text>().text = weapons[i].itemName;
                Panel.Find("Description").GetComponent<TMP_Text>().text =
                weapons[i].description + "\n\n" + "Damage to mobs: " + weapons[i].damageToMobs + "\n\n" + "Damage to blocks: " + weapons[i].damageToBlocks;
                Panel.Find("Image").GetComponent<Image>().sprite = weapons[i].icon;
                int j = weapons[i].itemID;
                Panel.Find("Equip").Find("Button").GetComponent<Button>().onClick.AddListener(() => EquipWeapon(j));
                if (weapons[i].isEquipped) {
                    Panel.Find("Equip").GetComponent<Toggle>().isOn = true;
                } else {
                    Panel.Find("Equip").GetComponent<Toggle>().isOn = false;
                }
            }

        }


    }

    public void DuplicateSkinElement()
    {
        GameObject duplicatedElement2 = Instantiate(skinItemTemplate, skinItemTemplate.transform.parent);
        duplicatedElement2.transform.SetSiblingIndex(skinItemTemplate.transform.GetSiblingIndex() + 1); // set the position of the duplicated element to be right after the original element
        duplicatedElement2.SetActive(true);
    }

    public void closePanels()
    {
        Transform parentObject = powerupItemTemplate.transform.parent;
        for (int i = 0; i < parentObject.transform.childCount; i++)
        {
            Transform child2 = parentObject.transform.GetChild(i);
            if (child2.name.Contains("Inventory Item(Clone)"))
            {
                Destroy(child2.gameObject);
            }
        }
    }

    // equips weapon based on item id
    public void EquipWeapon(int a) {
        for (int i = 0; i<weapons.Length; i++) {
            if(weapons[i].itemID == a) {
                GameManager.Instance.SetWeapon(a);
                weapons[i].isEquipped = true;
            } else {
                weapons[i].isEquipped = false;
            }
        }
        closePanels();
        loadPanels();
    }
}
