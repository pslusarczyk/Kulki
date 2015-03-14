using Tools;
using UnityEngine;

public class PlayerChaser : NpcBehaviour
{
  protected override Vector2 ComputeDirection()
   {
      var direction = new Vector2(
         PlayerObject().transform.position.x - transform.position.x,
         PlayerObject().transform.position.y - transform.position.y).normalized;
      this.LookAt2D(direction);
      return direction;
   }
}
