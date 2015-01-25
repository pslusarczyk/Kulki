using System.Linq;
using UnityEngine;
using System.Collections;

public class PlayerHandColliding : MonoBehaviour {

   public Rigidbody2D PlayerAnchor;

   private Rigidbody2D _catchedBody;

   void Update()
   {
      if (Input.GetKeyDown("space") && _catchedBody != null)
      {
         Destroy(PlayerAnchor.gameObject.GetComponents<DistanceJoint2D>()
            .First(c => c.connectedBody == _catchedBody));
         _catchedBody.GetComponent<PlayerChaser>().enabled = true;
         _catchedBody = null;
      }
   }

   void OnTriggerEnter2D(Collider2D metCollider)
   {
      if (_catchedBody == null && metCollider.gameObject.CompareTag("Catchable"))
      {
         _catchedBody = metCollider.rigidbody2D;
         _catchedBody.GetComponent<PlayerChaser>().enabled = false;
         var joint = PlayerAnchor.gameObject.AddComponent<DistanceJoint2D>();
         joint.connectedBody = _catchedBody;
         joint.distance = .9f;
      }
   }
}
