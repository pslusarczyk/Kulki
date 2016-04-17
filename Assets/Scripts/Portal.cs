using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{

    public Transform Exit;

    void OnTriggerEnter2D(Collider2D other)
    {
        other.transform.position = Exit.transform.position;
    }


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, Exit.transform.position);
    }
}
