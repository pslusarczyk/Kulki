using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
   public class FlowFieldEmitter : MonoBehaviour
   {
      private const bool _debug = false;
      public float Radius;
      
      public IEnumerable<Vector2> Attractors { get; set; }

      void Update()
      {
         const int attractionPointsCount = 24;
         IEnumerable<Vector2> attractionPoints = GetAttractionPoints(attractionPointsCount).ToList();
         Attractors = attractionPoints;
         foreach (var attractionPoint in attractionPoints)
         {
            if(_debug) Debug.DrawLine(GetComponent<Rigidbody2D>().position, attractionPoint);
         }
         
      }

      private IEnumerable<Vector2> GetAttractionPoints(int attractionPointsCount)
      {
         float angle = 360f/attractionPointsCount;
         for (int i = 0; i < attractionPointsCount; i++)
         {
            float radians = (angle*i*Mathf.Deg2Rad);
            float range = 6f;
            var direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)).normalized * range;
            if (Physics2D.RaycastAll(GetComponent<Rigidbody2D>().position + direction*.15f, direction, range).Count() < 2)
               yield return GetComponent<Rigidbody2D>().position + direction;
         }
      }
   }
}