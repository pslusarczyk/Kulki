using UnityEngine;

namespace Tools
{
   public static class ColorExtensions
   {
      public static void SetAlpha(this Material material, float value)
      {
         Color color = material.color;
         color.a = value;
         material.color = color;
      }
   }
}