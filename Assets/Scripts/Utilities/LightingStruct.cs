using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct LightingStruct
{
    public LightingStruct(Color ambientColor, Color directionalColor, float directionalIntensity)
    {
        this.ambientColor = ambientColor;
        this.directionalColor = directionalColor;
        this.directionalIntensity = directionalIntensity;
    }

    public Color ambientColor { get; }
    public Color directionalColor { get; }
    public float directionalIntensity { get; }
}
