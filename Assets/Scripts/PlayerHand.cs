using Tools;
using UnityEngine;
using System.Collections;

public class PlayerHand : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () {
      var inputX = Input.GetAxis("Horizontal2");
      var inputY = Input.GetAxis("Vertical2");
	   var handPosition = new Vector2(inputX, inputY);
	   const float minimalHandMagnitude = .2f;
      GetComponent<SpriteRenderer>().enabled = handPosition.sqrMagnitude > minimalHandMagnitude;
      GetComponent<SpriteRenderer>().material.SetAlpha(handPosition.sqrMagnitude);
	   transform.localPosition = handPosition * .5f;
	}
}
