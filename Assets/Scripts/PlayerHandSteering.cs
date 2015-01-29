using Assets;
using Tools;
using UnityEngine;

public class PlayerHandSteering : MonoBehaviour
{
   public PlayerHandColliding PlayerHandColliding;
   public EdgeCollider2D HostageObstacle;
   
	// Update is called once per frame
	void Update () {
      var inputX = Input.GetAxis("Horizontal2");
      var inputY = Input.GetAxis("Vertical2");
	   var handPosition = new Vector2(inputX, inputY);
	   const float minimalHandMagnitude = .2f;
      GetComponent<SpriteRenderer>().enabled = handPosition.sqrMagnitude > minimalHandMagnitude;
      GetComponent<SpriteRenderer>().material.SetAlpha(handPosition.sqrMagnitude);
	   transform.localPosition = handPosition * .5f;
	   PlayerHandColliding.transform.position = transform.position;

	   var hostage = PlayerHandColliding.GetComponent<PlayerHandColliding>().CatchedBody;
      HostageObstacle.GetComponent<EdgeCollider2D>().enabled = hostage != null;
      if (hostage != null && handPosition.sqrMagnitude > .1f)
      {
         PlayerHandColliding.gameObject.layer = Constants.Layers.Collider;
         var hostageToHand = transform.position - hostage.transform.position;
         hostage.AddForce(hostageToHand * 15);
	   }
	}
}
   