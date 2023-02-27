using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject targetObject; // Will be set by player controller scripts
    public float smoothing = 0.3f;
    public Vector3 offset = new Vector3(0, 15, -10);

    // This value will change at the runtime depending on target movement. Initialize with zero vector.
    private Vector3 velocity = Vector3.zero;

    public void setTarget(GameObject obj)
    {
        targetObject = obj;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        followObject();
        
    }

    void followObject()
    {
        if (targetObject == null) return;
        Vector3 targetPos = offset + targetObject.transform.position;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothing);

    }

}
