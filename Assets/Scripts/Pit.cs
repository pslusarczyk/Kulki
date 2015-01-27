using UnityEngine;
using System.Collections;

public class Pit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

   void OnTriggerEnter2D(Collider2D metCollider)
   {
      if (metCollider.gameObject.GetComponent<PlayerChaser>() != null)
      {
         Destroy(metCollider.gameObject);
      }
   }
}
