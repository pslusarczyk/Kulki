using System.Collections.Generic;
using System.Linq;
using Assets;
using Tools;
using UnityEngine;

public class PlayerHandSteering : MonoBehaviour
{
   public PlayerHandColliding PlayerHandColliding;
   public JumpBar JumpBar;


	// Update is called once per frame
	void FixedUpdate () {
      var inputX = Input.GetAxis("Horizontal2");
      var inputY = Input.GetAxis("Vertical2");

	   var handPosition = new Vector2(inputX, inputY);
	   const float minimalHandMagnitude = .2f;
      GetComponent<SpriteRenderer>().enabled = handPosition.sqrMagnitude > minimalHandMagnitude;
      GetComponent<SpriteRenderer>().material.SetAlpha(handPosition.sqrMagnitude);
	   transform.localPosition = handPosition * 0.5f;
	   PlayerHandColliding.transform.position = transform.position;

	   var hostages = PlayerHandColliding.CaughtBodies;

      if (hostages.Count > 0 && Input.GetKey(KeyCode.L))
	   {
         SpinHostages(hostages);
      }

      if (hostages.Count > 0 && handPosition.sqrMagnitude > .1f)
      {
         WaveHostages(hostages);
	   }
	}

   private void SpinHostages(List<Rigidbody2D> hostages)
   {
      if (JumpBar.Value <= 0.15) return;

      JumpBar.Value -= Time.fixedDeltaTime * 1.5f;

      foreach (var hostage in hostages)
      {
         Vector3 toPlayer = hostage.transform.position - PlayerHandColliding.Player.transform.position;
         float roundingAngle = 35f;
         Vector2 roundingToPlayer = Quaternion.AngleAxis(roundingAngle, Vector3.forward)*toPlayer;
         hostage.AddForce(roundingToPlayer*500);
      }
   }

   private void WaveHostages(List<Rigidbody2D> hostages)
   {
      foreach (var hostage in hostages)
      {
         var hostageToHand = transform.position - hostage.transform.position;
         hostage.AddForce(hostageToHand*20);
      }
   }
}
   