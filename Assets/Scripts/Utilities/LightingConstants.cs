using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingConstants : MonoBehaviour
{
   public static LightingStruct DEFAULT;
   public static LightingStruct CAVE = new LightingStruct(
      new Color(0.9f, 0.9f, 0.9f),
      new Color(1f, 1f, 1f),
      0f
   );
   public void Awake() {
      
      LightingStruct defaultLighting = new LightingStruct(
         RenderSettings.ambientLight,
         RenderSettings.sun.color,
         RenderSettings.sun.intensity
      );

      DEFAULT = defaultLighting;

   }



}
