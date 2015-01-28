using System.Linq;
using Assets;
using Tools;
using UnityEngine;

public class SmartPlayerChaser : PlayerChaser
{
   private Vector2 _seekDirection;
   private Vector2 _fleeDirection;

   public GameObject Pit;

   void OnDrawGizmos()
   {
      Gizmos.color = Color.green;
      Gizmos.DrawLine(transform.position, transform.position + new Vector3(_seekDirection.x, _seekDirection.y, 0));
      Gizmos.color = Color.red;
      Gizmos.DrawLine(transform.position, transform.position + new Vector3(_fleeDirection.x, _fleeDirection.y, 0));
   }

   protected override Vector2 ComputeDirection()
   {
      var probePoint = rigidbody2D.position + rigidbody2D.velocity;
      RaycastHit2D[] hits   = Physics2D.LinecastAll(rigidbody2D.position, probePoint, Constants.Layers.Solid);
      if (hits.Count() > 0)
      {
         var closestHit = hits.Aggregate((prev, next) =>
            prev == null || prev.distance < next.distance
               ? next
               : prev
            );

            var avoidanceForce = (rigidbody2D.velocity - closestHit.centroid).normalized;
            return avoidanceForce;
      }
      
      var target = PlayerObject().transform.position;
      Vector2 steeringVector;
     //if ((Pit.transform.position - transform.position).sqrMagnitude < 100f)
     //   steeringVector = Seek(target) + Flee(Pit.transform.position);
     //else
         steeringVector = Seek(target);
      return steeringVector.normalized;
   }

   private Vector2 Seek(Vector2 targetPosition)
   {
      var toTarget = new Vector2(
         targetPosition.x - transform.position.x,
         targetPosition.y - transform.position.y);
      var result = toTarget - rigidbody2D.velocity;
      _seekDirection = result;
      return result;
   }

   private Vector2 Flee(Vector2 sourcePosition)
   {
      var result = -Seek(sourcePosition);
      _fleeDirection = result;
      return result;
   }
}
