
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    public void OpenShop() {
        gameObject.transform.parent.Find("ShopUI").gameObject.SetActive(true);
        gameObject.SetActive(false);

    }

    public void OpenInventory() {
        gameObject.transform.parent.Find("Inventory").gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void StartGame() {
        gameObject.transform.parent.Find("InGameUI").gameObject.SetActive(true);
        gameObject.SetActive(false);
    
        GoToScene("Game Scene");
    }

    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Application has quit");
    }
}
