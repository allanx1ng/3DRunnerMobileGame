using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHelper : MonoBehaviour
{
    public static GameObject FindAncestorWithTag(GameObject obj, string tag) {
        // If the current object has the target tag, return the object
        if (obj == null || obj.CompareTag(tag))
        {
            return obj;
        }

        // If the current object has no parent, return null (top of the hierarchy)
        if (obj.transform.parent == null)
        {
            return null;
        }

        // Recursively call this function on the parent object
        return FindAncestorWithTag(obj.transform.parent.gameObject, tag);
    }
}
