using System.Collections.Generic;
using System.Linq;
using Assets;
using Tools;
using UnityEngine;

public class SmartPlayerChaser : PlayerChaser
{
   public GameObject Pit;
   public float AvoidanceCoefficient;
   private Vector2 _targetPosition;

   protected override Vector2 ComputeDirection()
   {
      Vector2 steeringVector = Vector2.zero;

      _targetPosition = PlayerObject().transform.position;
      steeringVector += Seek(_targetPosition);
      this.LookAt2D(steeringVector);

      var avoidanceForce = AvoidCollisions(steeringVector);
      steeringVector += avoidanceForce;

      return steeringVector.normalized;
   }

   private Vector2 AvoidCollisions(Vector2 steeringVector)
   {
      Vector2 avoidanceForce = Vector2.zero;
      IEnumerable<RaycastHit2D> hits = RayCastFrom(new Vector2(0, 0));
      IEnumerable<RaycastHit2D> hitsCloserThanTarget = hits.Where(
         h => (rigidbody2D.position - h.point).sqrMagnitude < (rigidbody2D.position - _targetPosition).sqrMagnitude).ToList();
      if (hitsCloserThanTarget.Any())
      {
         var closestHit = hitsCloserThanTarget.Aggregate((prev, next) =>
            prev == null || prev.distance < next.distance
               ? prev
               : next
            );

         Vector2 hitObjectCenter = closestHit.transform.position;
         var penetration = (hitObjectCenter - closestHit.point).magnitude
                     - Vector3.Cross(rigidbody2D.velocity.normalized, hitObjectCenter - rigidbody2D.position).magnitude;
         var velocity = rigidbody2D.velocity;
         var facingDirection = steeringVector;
         Vector2 avoidanceDirection;
         // angle between facing the direction and the normal of the hit
         var relativeCollisionAngle = facingDirection.x*closestHit.normal.y + -facingDirection.y*closestHit.normal.x;
         if (relativeCollisionAngle >= 0)
            avoidanceDirection = new Vector2(-velocity.y, velocity.x); // 90 degrees to the left
         else
            avoidanceDirection = new Vector2(velocity.y, -velocity.x); // 90 degrees to the right
         avoidanceForce = avoidanceDirection*penetration*MaxVelocitySqr*AvoidanceCoefficient;
      }
      return avoidanceForce;
   }

   private Vector2 Seek(Vector2 targetPosition)
   {
      var desiredVelocity = (targetPosition-rigidbody2D.position).normalized * MaxVelocitySqr;
      return desiredVelocity - rigidbody2D.velocity;
   }

   private RaycastHit2D[] RayCastFrom(Vector2 transposition)
   {
      return Physics2D.RaycastAll(rigidbody2D.position + transposition, rigidbody2D.velocity, rigidbody2D.velocity.magnitude, 
         Constants.Layers.Collider);
   }

   private Vector2 Flee(Vector2 sourcePosition)
   {
      return -Seek(sourcePosition);
   }
}
