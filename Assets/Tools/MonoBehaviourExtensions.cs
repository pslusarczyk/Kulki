using UnityEngine;

namespace Tools
{
   public static class MonoBehaviourExtensions
   {
      public static void LookAt2D(this MonoBehaviour mb, float x, float y)
      {
         var angle = Mathf.Atan2(y, x)*Mathf.Rad2Deg;
         mb.GetComponent<Rigidbody2D>().rotation = angle - 90;
      }

      public static void LookAt2D(this MonoBehaviour mb, Vector2 target)
      {
         var angle = Mathf.Atan2(target.y, target.x)*Mathf.Rad2Deg;
         mb.GetComponent<Rigidbody2D>().rotation = angle - 90;
      }
   }
}