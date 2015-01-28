using Tools;
using UnityEngine;

public class Steering : MonoBehaviour
{
   public float MaxVelocitySqr = 2;
   public float Kick;
   public GameObject JumpBarObject;
   private const float JumpForceMultiplier = 60;


	// Use this for initialization
	void Start ()
	{
      
	}

   void Update()
   {
      if (Input.GetKeyDown(KeyCode.T))
         Time.timeScale = (Time.timeScale*.5f);

   }
	
	void FixedUpdate ()
	{
	   var inputX = Input.GetAxis("Horizontal");
      var inputY = Input.GetAxis("Vertical");
	   var inputAndSpeedSignsAreSameForX = !(rigidbody2D.velocity.x * inputX < 0);
	   var inputAndSpeedSignsAreSameForY = !(rigidbody2D.velocity.y * inputY < 0);
      var forceX = Mathf.Abs(rigidbody2D.velocity.x) > MaxVelocitySqr && inputAndSpeedSignsAreSameForX ? 0 : inputX;
      var forceY = Mathf.Abs(rigidbody2D.velocity.y) > MaxVelocitySqr && inputAndSpeedSignsAreSameForY ? 0 : inputY;
	   
      var forceFactor = Kick*Time.fixedDeltaTime*rigidbody2D.mass;
      var forceToAdd = new Vector2(forceX, forceY) * forceFactor;
	   if (Input.GetKey(KeyCode.K))
	   {
         forceToAdd += forceToAdd.normalized * forceFactor 
                              * JumpBarObject.GetComponent<JumpBar>().Value * JumpForceMultiplier;
         JumpBarObject.GetComponent<JumpBar>().Value = 0;
	   }
      rigidbody2D.AddForce(forceToAdd);
	}
}
