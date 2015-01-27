using UnityEngine;
using System.Collections;

public class Breeder : MonoBehaviour
{

   public float OptimalDistanceToPlayer;

	// Use this for initialization
	void Start () {
	   InvokeRepeating("Breed", 1f, 1f);
	}
	
	// Update is called once per frame
	void Update ()
	{
	   var playerChaser = GetComponent<PlayerChaser>();
	   float toPlayer = (playerChaser.PlayerObject().transform.position - transform.position).magnitude;
	   playerChaser.enabled = toPlayer > OptimalDistanceToPlayer;


	}

   void Breed()
   {
      Instantiate(Resources.Load<PlayerChaser>("Prefabs/PlayerChaser"), transform.position, Quaternion.identity);
   }
}
