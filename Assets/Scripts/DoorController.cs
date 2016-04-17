using System;
using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    public string OpenerComponentName = String.Empty;

    public Rigidbody2D Door;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent(OpenerComponentName) != null)
        {
            Door.AddRelativeForce(new Vector2(0, 500));
        }
    }


    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        Door.AddRelativeForce(new Vector2(0, -10));
    }
}
