using UnityEngine;
using System.Collections;

public class Breeder : MonoBehaviour {

	// Use this for initialization
	void Start () {
	InvokeRepeating("Breed", 1f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
	   
	}

   void Breed()
   {
      Instantiate(Resources.Load<PlayerChaser>("Prefabs/PlayerChaser"), transform.position, transform.rotation);
      Debug.Log(Time.deltaTime);
   }
}
