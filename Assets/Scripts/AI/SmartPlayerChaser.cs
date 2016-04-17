using System.Collections.Generic;
using System.Linq;
using Assets;
using Assets.Scripts;
using Tools;
using UnityEngine;

public class SmartPlayerChaser : NpcBehaviour
{
   private const bool _debug = true;
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

      var flowFieldForce = FlowFieldForce();
      steeringVector += flowFieldForce;

      

      return steeringVector.normalized;
   }

   private Vector2 AvoidCollisions(Vector2 steeringVector)
   {
      Vector2 avoidanceForce = Vector2.zero;
      IEnumerable<RaycastHit2D> hits = RayCastFrom(new Vector2(0, 0));
      IEnumerable<RaycastHit2D> properHits = hits.Where(
         h => (GetComponent<Rigidbody2D>().position - h.point).sqrMagnitude < (GetComponent<Rigidbody2D>().position - _targetPosition).sqrMagnitude // closer then target
         && (GetComponent<Rigidbody2D>().position - h.point).sqrMagnitude > .1f) // not too close
         .ToList();
      if (properHits.Any())
      {
         var closestHit = properHits.Aggregate((prev, next) =>
            prev == null || prev.distance < next.distance
               ? prev
               : next
            );

         // Vector2 hitObjectCenter = closestHit.transform.position;
         // var penetration = (hitObjectCenter - closestHit.point).magnitude
         //       - Vector3.Cross(rigidbody2D.velocity.normalized, hitObjectCenter - rigidbody2D.position).magnitude;
         // Buckman recomended the above for computing penetration of circle-like objects and the below for wall-like
         var penetration = GetComponent<Rigidbody2D>().velocity.magnitude - (GetComponent<Rigidbody2D>().position - closestHit.point).magnitude;
         var velocity = GetComponent<Rigidbody2D>().velocity;
         Vector2 avoidanceDirection;
         // angle between the velocity and the normal of the hit
         var relativeCollisionAngle = velocity.x * closestHit.normal.y + -velocity.y * closestHit.normal.x;
         if (relativeCollisionAngle >= 0)
            avoidanceDirection = new Vector2(-velocity.y, velocity.x); // 90 degrees to the left
         else
            avoidanceDirection = new Vector2(velocity.y, -velocity.x); // 90 degrees to the right
         avoidanceForce = avoidanceDirection*penetration*MaxVelocitySqr*AvoidanceCoefficient;
         if(_debug) Debug.DrawLine(GetComponent<Rigidbody2D>().position, closestHit.point, Color.cyan);
      }
      if (_debug) Debug.DrawLine(GetComponent<Rigidbody2D>().position, GetComponent<Rigidbody2D>().position + steeringVector, Color.green);
      if (_debug) Debug.DrawLine(GetComponent<Rigidbody2D>().position, GetComponent<Rigidbody2D>().position + avoidanceForce, Color.blue);
      return avoidanceForce;
   }

   protected virtual Vector2 Seek(Vector2 targetPosition)
   {
       Vector2 toPlayer = targetPosition - GetComponent<Rigidbody2D>().position;
        

        var desiredVelocity = (toPlayer).normalized * MaxVelocitySqr;
      return desiredVelocity - GetComponent<Rigidbody2D>().velocity;
   }

   protected virtual Vector2 FlowFieldForce()
   {
      var playerFlowField = PlayerObject().GetComponent<FlowFieldEmitter>();
      if (playerFlowField.enabled
          && (PlayerObject().GetComponent<Rigidbody2D>().position - GetComponent<Rigidbody2D>().position).magnitude < playerFlowField.Radius)
      {
         var forceFieldForce = (GetComponent<Rigidbody2D>().position - PlayerObject().GetComponent<Rigidbody2D>().position) * 1000;

         var attractionForce = Vector2.zero;
         foreach (var attractor in playerFlowField.Attractors)
         {
            var force = (attractor - GetComponent<Rigidbody2D>().position).normalized * (attractor - GetComponent<Rigidbody2D>().position).sqrMagnitude*7.5f;
            attractionForce += force;
         }
         forceFieldForce += attractionForce;
         if(_debug) Debug.DrawLine(GetComponent<Rigidbody2D>().position, GetComponent<Rigidbody2D>().position + attractionForce, Color.green);
         return forceFieldForce;
      }
      return Vector2.zero;
   }

   private RaycastHit2D[] RayCastFrom(Vector2 transposition)
   {
      return Physics2D.RaycastAll(GetComponent<Rigidbody2D>().position + transposition, GetComponent<Rigidbody2D>().velocity, GetComponent<Rigidbody2D>().velocity.magnitude, 
         Constants.Layers.ColliderMask);
   }

   private Vector2 Flee(Vector2 sourcePosition)
   {
      return -Seek(sourcePosition);
   }
}
