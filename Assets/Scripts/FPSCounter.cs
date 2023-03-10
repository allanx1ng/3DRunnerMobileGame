using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private GUIStyle guiStyle = new GUIStyle(); //create a new variable
    void OnGUI()
    {
        guiStyle.fontSize = 50; 
        GUI.Label(new Rect(0, 0, 100, 100), (1.0f / Time.smoothDeltaTime).ToString(), guiStyle);        
    }
}
