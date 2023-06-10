using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    // Start is called before the first frame update

    public Slider progressBar;
    public GameObject text;
    public Image panel;
    public GameObject LoadUI;


    void Start()
    {
        if (PlayerPrefs.GetInt("ShouldStartGame") != 1 && PlayerPrefs.GetInt("ShouldStartGame") != 2) {
            StartCoroutine(LoadGame());
        } else {
            LoadUI.SetActive(false);
        }

        
        
        
    }

    private System.Collections.IEnumerator LoadGame()
    {
        float totalTime = 0f;
        float progress = 0f;

        while (progress < 1f)
        {
            yield return new WaitForSecondsRealtime(0.1f);

            if (Random.value <= 0.5f)
            {
                progress += 0.1f;
                progressBar.value = progress;
            }

            totalTime += 0.1f;
        }

        progressBar.gameObject.SetActive(false);
        text.SetActive(false);


        Color imageColor = panel.color;

        while (imageColor.a > 0f)
        {
            yield return new WaitForSecondsRealtime(0.01f);

            float newAlpha = imageColor.a - 0.01f;
            imageColor = new Color(imageColor.r, imageColor.g, imageColor.b, newAlpha);
            panel.color = imageColor;
            
        }

        LoadUI.SetActive(false);
        
    }
}
