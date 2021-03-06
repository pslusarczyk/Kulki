﻿using Tools;
using UnityEngine;

public class Steering : MonoBehaviour
{
   public float MaxVelocitySqr = 2;
   public float Kick;
   public JumpBar JumpBar;
   private const float JumpForceMultiplier = 60;


	// Use this for initialization
	void Start ()
	{
      
	}

   void Update()
   {
      if (Input.GetKeyDown(KeyCode.T) && Input.GetKeyDown(KeyCode.RightShift))
         Time.timeScale = (Time.timeScale*2f);
      else if (Input.GetKeyDown(KeyCode.T))
         Time.timeScale = (Time.timeScale*.5f);

   }
	
	void FixedUpdate ()
	{
	   var inputX = Input.GetAxis("Horizontal");
      var inputY = Input.GetAxis("Vertical");
      var input = new Vector2(inputX, inputY);
	   var inputAndSpeedSignsAreSameForX = !(GetComponent<Rigidbody2D>().velocity.x * inputX < 0);
	   var inputAndSpeedSignsAreSameForY = !(GetComponent<Rigidbody2D>().velocity.y * inputY < 0);
      var forceX = Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > MaxVelocitySqr && inputAndSpeedSignsAreSameForX ? 0 : inputX;
      var forceY = Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) > MaxVelocitySqr && inputAndSpeedSignsAreSameForY ? 0 : inputY;
	   
      var forceFactor = Kick*Time.fixedDeltaTime*GetComponent<Rigidbody2D>().mass;
      var forceToAdd = new Vector2(forceX, forceY) * forceFactor;
	   if (Input.GetKey(KeyCode.K))
	   {
         forceToAdd += input.normalized * forceFactor 
                              * JumpBar.Value * JumpForceMultiplier;
         JumpBar.Value = 0;
	   }
	   if (Input.GetKeyDown(KeyCode.C))
	   {
         Instantiate(Resources.Load("Prefabs/Player"), transform.position, Quaternion.identity);
      }
      GetComponent<Rigidbody2D>().AddForce(forceToAdd);
	}
}
