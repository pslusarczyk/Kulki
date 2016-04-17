using System.Linq;
using UnityEngine;
using System.Collections;
using Assets;

public class PlayerHandColliding : MonoBehaviour {

   public Rigidbody2D PlayerAnchor;

   private Rigidbody2D _catchedBody;

   public Rigidbody2D CatchedBody
   {
      get { return _catchedBody; }
   }

   void Update()
   {
      if (Input.GetKeyDown("space") && CatchedBody != null)
      {
         PlayerAnchor.gameObject.GetComponent<DistanceJoint2D>().connectedBody = null;
         PlayerAnchor.gameObject.GetComponent<DistanceJoint2D>().enabled = false;
         CatchedBody.GetComponent<SmartPlayerChaser>().enabled = true;
         Destroy(CatchedBody.transform.GetChild(0).gameObject);
         StartCoroutine(Example());
      }
   }

   IEnumerator Example()
   {
      yield return new WaitForSeconds(.5f);
      _catchedBody = null;
   }

   void OnTriggerEnter2D(Collider2D metCollider)
   {
      if (metCollider.gameObject.CompareTag("Catchable"))
      {
         if (!(CatchedBody == null && Input.GetKey("space")))
            return;
         _catchedBody = metCollider.GetComponent<Rigidbody2D>();
         CatchedBody.GetComponent<SmartPlayerChaser>().enabled = false;
         GameObject highlight = (GameObject)Instantiate(Resources.Load("Prefabs/CatchedHighlight"), 
            CatchedBody.transform.position, Quaternion.identity);
         highlight.transform.parent = CatchedBody.transform;
         var joint = PlayerAnchor.gameObject.GetComponent<DistanceJoint2D>();
         joint.autoConfigureDistance = false;
         joint.connectedBody = CatchedBody;
         joint.distance = .8f;
         joint.connectedAnchor = Vector2.zero;
         joint.enabled = true;
      }
   }
}
