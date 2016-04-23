using System;
using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets;

public class PlayerHandColliding : MonoBehaviour {

   public GameObject Player;

   private bool _canCatch = true;

   private List<Rigidbody2D> _caughtBodies = new List<Rigidbody2D>();

   public List<Rigidbody2D> CaughtBodies
   {
      get { return _caughtBodies; }
   }

   void Update()
   {
      if (Input.GetKeyDown(KeyCode.R))
      {
         foreach (DistanceJoint2D playerJoint in Player.gameObject.GetComponents<DistanceJoint2D>())
         {
          playerJoint.connectedBody = null;
          playerJoint.enabled = false;
         }
         foreach (var caughtBody in _caughtBodies)
         {
            caughtBody.GetComponent<SmartPlayerChaser>().enabled = true;
            Destroy(caughtBody.transform.GetChild(0).gameObject);
         }
         _caughtBodies = new List<Rigidbody2D>();
         _canCatch = true;
      }
   }

   void OnTriggerStay2D(Collider2D metCollider)
   {
      if (_canCatch == false || !Input.GetKey("space"))
         return;

      if (metCollider.gameObject.CompareTag("Catchable"))
      {
         Rigidbody2D body = metCollider.GetComponent<Rigidbody2D>();

         if (_caughtBodies.Contains(body))
            return;

         var joint = Player.GetComponents<DistanceJoint2D>().FirstOrDefault(j => j.enabled == false);
         if (joint == null)
         {
            _canCatch = false;
            return;
         }

         _caughtBodies.Add(body);
         body.GetComponent<SmartPlayerChaser>().enabled = false;
         GameObject highlight = (GameObject)Instantiate(Resources.Load("Prefabs/CatchedHighlight"),
            body.transform.position, Quaternion.identity);
         highlight.transform.parent = body.transform;

         joint.autoConfigureDistance = false;
         joint.connectedBody = body;
         joint.distance = .8f;
         joint.connectedAnchor = Vector2.zero;
         joint.enabled = true;
      }
   }
}
