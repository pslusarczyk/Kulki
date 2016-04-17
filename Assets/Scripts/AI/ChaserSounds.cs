using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class ChaserSounds : MonoBehaviour
{
    protected GameObject _playerObject;

    public AudioClip bark;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
        var playerFlowField = PlayerObject().GetComponent<FlowFieldEmitter>();
	    if (playerFlowField.enabled
	        &&
	        (PlayerObject().GetComponent<Rigidbody2D>().position - GetComponent<Rigidbody2D>().position).magnitude <
	        playerFlowField.Radius)
	        
	    return;
	}

    public GameObject PlayerObject()
    {
        if (_playerObject == null)
        {
            _playerObject = GameObject.Find("Player");
        }
        return _playerObject;
    }
}
