using UnityEngine;

public class Steering : MonoBehaviour
{
   public float MaxVelocitySqr = 2;
   public float Kick = 240000;
   

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	   var inputX = Input.GetAxis("Horizontal");
      var inputY = Input.GetAxis("Vertical");
	   var inputAndSpeedSignsAreSameForX = !(rigidbody2D.velocity.x * inputX < 0);
	   var inputAndSpeedSignsAreSameForY = !(rigidbody2D.velocity.y * inputY < 0);
      var x = Mathf.Abs(rigidbody2D.velocity.x) > MaxVelocitySqr && inputAndSpeedSignsAreSameForX ? 0 : inputX;
      var y = Mathf.Abs(rigidbody2D.velocity.y) > MaxVelocitySqr && inputAndSpeedSignsAreSameForY ? 0 : inputY;
	   var forceToAdd = new Vector2(x, y) * Kick * Time.fixedDeltaTime * rigidbody2D.mass;
      rigidbody2D.AddForce(forceToAdd);
	}
}
