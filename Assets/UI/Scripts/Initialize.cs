using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Initialize : MonoBehaviour
{

    // All UIs under canvas
    public GameObject[] UIList;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i<UIList.Length;i++) {
            if (UIList[i].name == "Panel") {
                UIList[i].SetActive(true);
            } else {
                UIList[i].SetActive(false);
            }
            
        }
    }

}
