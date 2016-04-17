using Tools;
using UnityEngine;

public abstract class NpcBehaviour : MonoBehaviour
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
      var inputAndSpeedSignsAreSameForX = !(GetComponent<Rigidbody2D>().velocity.x * directionX < 0);
      var inputAndSpeedSignsAreSameForY = !(GetComponent<Rigidbody2D>().velocity.y * directionY < 0);
      var x = Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > MaxVelocitySqr && inputAndSpeedSignsAreSameForX ? 0 : directionX;
      var y = Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) > MaxVelocitySqr && inputAndSpeedSignsAreSameForY ? 0 : directionY;
      
      Vector2 forceToAdd = new Vector2(x, y).normalized * Kick * Time.fixedDeltaTime * GetComponent<Rigidbody2D>().mass;
      GetComponent<Rigidbody2D>().AddForce(forceToAdd);
   }

   void OnCollisionEnter2D(Collision2D collision)
   {
      if (collision.relativeVelocity.sqrMagnitude > 150)
         _stunTime = 3;
   }

   protected abstract Vector2 ComputeDirection();
}