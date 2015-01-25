using UnityEngine;

public class PlayerChaser : MonoBehaviour
{
   private GameObject _playerObject;

   public float MaxVelocitySqr;
   public float Kick;

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

   // Update is called once per frame
   void FixedUpdate()
   {
      var xToPlayer = PlayerObject().transform.position.x - transform.position.x;
      var yToPlayer = PlayerObject().transform.position.y - transform.position.y;
      var inputAndSpeedSignsAreSameForX = !(rigidbody2D.velocity.x * xToPlayer < 0);
      var inputAndSpeedSignsAreSameForY = !(rigidbody2D.velocity.y * yToPlayer < 0);
      var x = Mathf.Abs(rigidbody2D.velocity.x) > MaxVelocitySqr && inputAndSpeedSignsAreSameForX ? 0 : xToPlayer;
      var y = Mathf.Abs(rigidbody2D.velocity.y) > MaxVelocitySqr && inputAndSpeedSignsAreSameForY ? 0 : yToPlayer;
      var forceToAdd = new Vector2(x, y) * Kick * Time.fixedDeltaTime;
      rigidbody2D.AddForce(forceToAdd);
   }
}
