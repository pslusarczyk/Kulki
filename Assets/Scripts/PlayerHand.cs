using System.Linq;
using Tools;
using UnityEngine;
using System.Collections;

public class PlayerHand : MonoBehaviour
{
   public Rigidbody2D PlayerAnchor;

   private Rigidbody2D _catchedBody;

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

      if (Input.GetKeyDown("space") && _catchedBody != null)
      {
         Destroy(PlayerAnchor.gameObject.GetComponents<DistanceJoint2D>()
            .First(c => c.connectedBody == _catchedBody));
	      _catchedBody = null;
	   }
	}

   void OnCollisionEnter2D(Collision2D collision)
   {
      if (_catchedBody == null && collision.gameObject.CompareTag("Catchable"))
      {
         collision.gameObject.transform.localScale *= 2;
         _catchedBody = collision.rigidbody;
         var joint = PlayerAnchor.gameObject.AddComponent<DistanceJoint2D>();
         joint.connectedBody = _catchedBody;
         joint.distance = .8f;
      }

   }
}
