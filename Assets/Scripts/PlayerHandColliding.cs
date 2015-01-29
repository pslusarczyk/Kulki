using System.Linq;
using UnityEngine;
using System.Collections;

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
         Destroy(PlayerAnchor.gameObject.GetComponents<DistanceJoint2D>()
            .First(c => c.connectedBody == CatchedBody));
         CatchedBody.GetComponent<PlayerChaser>().enabled = true;
         _catchedBody = null;
      }
   }

   void OnTriggerEnter2D(Collider2D metCollider)
   {
      if (CatchedBody == null && metCollider.gameObject.CompareTag("Catchable"))
      {
         _catchedBody = metCollider.rigidbody2D;
         CatchedBody.GetComponent<PlayerChaser>().enabled = false;
         var joint = PlayerAnchor.gameObject.AddComponent<DistanceJoint2D>();
         joint.connectedBody = CatchedBody;
         joint.distance = .9f;
      }
   }
}
