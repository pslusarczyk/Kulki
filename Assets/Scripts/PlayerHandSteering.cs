using Tools;
using UnityEngine;

public class PlayerHandSteering : MonoBehaviour
{
   public PlayerHandColliding PlayerHandColliding;
   
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
	}
}
