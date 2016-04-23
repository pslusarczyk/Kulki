using System;
using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
   private bool IsOpen = false;
   public Animator DoorAnimator;
   public string OpenerComponentName = String.Empty;

   // Use this for initialization
   void Start ()
	{
	   
	}

   void OnTriggerEnter2D(Collider2D coll)
   {
      if (coll.GetComponent(OpenerComponentName) != null)
      {
         IsOpen = true;
         TriggerAnimation("Open");
      }
   }

   void OnTriggerExit2D(Collider2D coll)
   {
      if (coll.GetComponent(OpenerComponentName) != null)
      {
         IsOpen = false;
         TriggerAnimation("Close");
      }
   }

   private void TriggerAnimation(string direction)
   {
      DoorAnimator.SetTrigger(direction);
   }
}
