using Tools;
using UnityEngine;

public class PlayerChaser : MonoBehaviour
{
   protected GameObject _playerObject;

   public float MaxVelocitySqr;
   public float Kick;

   private float _stunTime = 0f;

   public GameObject PlayerObject()
   {
      if (_playerObject == null)
      {
         _playerObject = GameObject.Find("Player");
      }
      return _playerObject;
   }

   // Use this for initialization
   void Start()
   {
   }

   void Update()
   {
      if (_stunTime > 0)
         _stunTime -= Time.deltaTime;
   }

   void FixedUpdate()
   {
      if (_stunTime > 0)
         return;

      Vector2 direction = ComputeDirection();
      var directionX = direction.x;
      var directionY = direction.y;
      var inputAndSpeedSignsAreSameForX = !(rigidbody2D.velocity.x * directionX < 0);
      var inputAndSpeedSignsAreSameForY = !(rigidbody2D.velocity.y * directionY < 0);
      var x = Mathf.Abs(rigidbody2D.velocity.x) > MaxVelocitySqr && inputAndSpeedSignsAreSameForX ? 0 : directionX;
      var y = Mathf.Abs(rigidbody2D.velocity.y) > MaxVelocitySqr && inputAndSpeedSignsAreSameForY ? 0 : directionY;
      
      Vector2 forceToAdd = new Vector2(x, y).normalized * Kick * Time.fixedDeltaTime * rigidbody2D.mass;
      rigidbody2D.AddForce(forceToAdd);
   }

   protected virtual Vector2 ComputeDirection()
   {
      var direction = new Vector2(
         PlayerObject().transform.position.x - transform.position.x,
         PlayerObject().transform.position.y - transform.position.y).normalized;
      this.LookAt2D(direction);
      return direction;
   }

   void OnCollisionEnter2D(Collision2D collision)
   {
      if (collision.relativeVelocity.sqrMagnitude > 150)
         _stunTime = 3;
   }
}
