using UnityEditor;
using UnityEngine;
using System.Collections;

public class PlayerChaser : MonoBehaviour
{
   public float MaxVelocitySqr = 1;
   public float Kick = 80;

   // Use this for initialization
   void Start()
   {
   }

   // Update is called once per frame
   void FixedUpdate()
   {
      var xToPlayer = 0 - transform.position.x;//GameObject.Find("Player").transform.position.x - transform.position.x;
      var yToPlayer = 0 - transform.position.y;//GameObject.Find("Player").transform.position.y - transform.position.y;
      var inputAndSpeedSignsAreSameForX = !(rigidbody2D.velocity.x * xToPlayer < 0);
      var inputAndSpeedSignsAreSameForY = !(rigidbody2D.velocity.y * yToPlayer < 0);
      var x = Mathf.Abs(rigidbody2D.velocity.x) > MaxVelocitySqr && inputAndSpeedSignsAreSameForX ? 0 : xToPlayer;
      var y = Mathf.Abs(rigidbody2D.velocity.y) > MaxVelocitySqr && inputAndSpeedSignsAreSameForY ? 0 : yToPlayer;
      var forceToAdd = new Vector2(x, y) * Kick * Time.fixedDeltaTime;
      rigidbody2D.AddForce(forceToAdd);
   }
}
