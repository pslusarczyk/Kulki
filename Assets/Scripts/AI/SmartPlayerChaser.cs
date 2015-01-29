using System.Collections.Generic;
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
   //   Gizmos.DrawLine(transform.position, transform.position + new Vector3(_seekDirection.x, _seekDirection.y, 0));
      Gizmos.color = Color.blue;
    //  Gizmos.DrawLine(transform.position, rigidbody2D.position + rigidbody2D.velocity);
      Gizmos.color = Color.red;
    //  Gizmos.DrawLine(transform.position, transform.position + new Vector3(_fleeDirection.x, _fleeDirection.y, 0));
   }

   protected override Vector2 ComputeDirection()
   {
      Vector2 steeringVector = Vector2.zero;

      var target = PlayerObject().transform.position;
      steeringVector += Seek(target);
      this.LookAt2D(steeringVector);
      IEnumerable<RaycastHit2D> hits = RayCastFrom(new Vector2(0, 0));
      //  RayCastFrom(new Vector2(-.3f, 0)).Union(
      //  RayCastFrom(new Vector2(+.3f, 0)));
      if (hits.Any())
      {
         var closestHit = hits.Aggregate((prev, next) =>
            prev == null || prev.distance < next.distance
               ? prev
               : next
            );

         Vector2 hitCenter = closestHit.transform.position;
         //.Debug.DrawRay(hitCenter, hitCenter + Vector2.up * .1f, Color.yellow, 1);
         //Debug.Log();
         //Debug.Log(); 
         //Debug.Log("d r");
         var depth = (hitCenter - closestHit.point).magnitude 
            - Vector3.Cross(rigidbody2D.velocity.normalized, hitCenter - rigidbody2D.position).magnitude;
         //Debug.Log(depth);
         var velocity = rigidbody2D.velocity;
         var seekDirection = steeringVector;
         Vector2 avoidanceDirection = Vector2.zero;
         var relativeCollisionAngle = seekDirection.x * closestHit.normal.y + -seekDirection.y * closestHit.normal.x;
         //Debug.Log(relativeCollisionAngle);
         if(relativeCollisionAngle >= 0)
            avoidanceDirection = new Vector2(-velocity.y, velocity.x);
         else
            avoidanceDirection = new Vector2(velocity.y, -velocity.x);
         var avoidanceForce = avoidanceDirection * depth * MaxVelocitySqr * 5;
         steeringVector += avoidanceForce;
         float distanceToCollision = (closestHit.point - rigidbody2D.position).magnitude;
         if (distanceToCollision < rigidbody2D.velocity.magnitude*0.5)
         {
          //  var breakForce = -rigidbody2D.velocity/distanceToCollision*MaxVelocitySqr;
          //  steeringVector += breakForce;
          //  //.Debug.DrawLine(rigidbody2D.position, breakForce, Color.red);
         }
         //.Debug.DrawLine(rigidbody2D.position, rigidbody2D.position + avoidanceForce, Color.blue);
         
      }
     // hits.ToList().ForEach(h => //.Debug.DrawRay(rigidbody2D.position, h.point - rigidbody2D.position, Color.red));
      
     //if ((Pit.transform.position - transform.position).sqrMagnitude < 100f)
     //   steeringVector = Seek(target) + Flee(Pit.transform.position);
     //else
      
         

         //.Debug.DrawLine(rigidbody2D.position, rigidbody2D.position + Seek(target), Color.magenta);
      return steeringVector.normalized;
   }

   private RaycastHit2D[] RayCastFrom(Vector2 transposition)
   {
      //.Debug.DrawRay(
      //  transform.TransformPoint(transposition),
      //  rigidbody2D.velocity, Color.cyan, .001f);
      return Physics2D.RaycastAll(rigidbody2D.position + transposition, rigidbody2D.velocity, rigidbody2D.velocity.magnitude, 
         Constants.Layers.Collider);
   }

   private Vector2 Seek(Vector2 targetPosition)
   {
      var desiredVelocity = (targetPosition-rigidbody2D.position).normalized * MaxVelocitySqr;
      var result = desiredVelocity - rigidbody2D.velocity;
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
