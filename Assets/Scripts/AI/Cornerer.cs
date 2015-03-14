using UnityEngine;
using System.Collections;

public class Cornerer : NpcBehaviour
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

   protected override Vector2 ComputeDirection()
   {
      return Vector2.right;
   }
}
