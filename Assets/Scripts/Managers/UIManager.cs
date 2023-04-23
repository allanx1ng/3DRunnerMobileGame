using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;
    
    [Header("Health UI")]
    [SerializeField] private List<Image> hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    public GameObject deathMenuObject;

    [Header("Menu Instances")]
    private DeathMenu deathMenu;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            deathMenu = deathMenuObject.GetComponent<DeathMenu>();
        }
        else
        {
            Debug.LogError("Multiple instances of UIManager detected. Destroying the duplicate.");
            Destroy(gameObject);
        }
    }

    public void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    public void ToggleDeathMenu(bool state) {
        deathMenu.ToggleDeathMenu(state);
    }
}
