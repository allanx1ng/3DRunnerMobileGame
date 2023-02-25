using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject targetObject; // Will be set by player controller scripts
    public float smoothing = 5f;
    public Vector3 offset = new Vector3(0, 15, -10);


    public void setTarget(GameObject obj)
    {
        targetObject = obj;
    }

    // Update is called once per frame
    void Update()
    {
        followObject();
        
    }

    void followObject()
    {
        if (targetObject == null) return;
        Vector3 targetPos = offset + targetObject.transform.position;
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
    }

}
