using UnityEngine;

public static class BoundsHelper
{
    public static Bounds GetBounds(GameObject gameObject)
    {
        Bounds bounds;

        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        SkinnedMeshRenderer skinnedMeshRenderer = gameObject.GetComponent<SkinnedMeshRenderer>();
        Collider collider = gameObject.GetComponent<Collider>();

        if (meshRenderer != null)
        {
            // Get bounds from MeshRenderer
            bounds = meshRenderer.bounds;
        }
        else if (skinnedMeshRenderer != null)
        {
            // Get bounds from SkinnedMeshRenderer
            bounds = skinnedMeshRenderer.bounds;
        }
        else if (collider != null)
        {
            // Get bounds from Collider
            bounds = collider.bounds;
        }
        else
        {
            // If none of the above components are present, return an empty bounds
            bounds = new Bounds(Vector3.zero, Vector3.zero);
        }

        return bounds;
    }
}