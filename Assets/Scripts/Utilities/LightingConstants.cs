using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingConstants : MonoBehaviour
{
   public static LightingStruct DEFAULT;
   public static LightingStruct CAVE = new LightingStruct(
      new Color(0.11f, 0.11f, 0.11f),
      new Color(1f, 1f, 1f),
      0.0f
   );

   public static bool initialized = false;
   public void Awake() {
      if (initialized) return;
      initialized = true;

      DEFAULT = new LightingStruct(
         RenderSettings.ambientLight,
         RenderSettings.sun.color,
         RenderSettings.sun.intensity
      );

      Debug.Log(RenderSettings.ambientLight.r);

      DontDestroyOnLoad(gameObject);
   }



}
