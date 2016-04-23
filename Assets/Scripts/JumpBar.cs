using UnityEngine;
using System.Collections;

public class JumpBar : MonoBehaviour
{
   public Color LoadingColor;
   public Color FullColor;

   public float Value;

   // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	   GetComponent<SpriteRenderer>().enabled = Value > 0;
      GetComponent<SpriteRenderer>().color = Value > .99 ? FullColor : LoadingColor;

      var inputX = Input.GetAxis("Horizontal");
      var inputY = Input.GetAxis("Vertical");

      var scale = transform.localScale;
      transform.localScale = new Vector3(scale.x, Value * .25f - .01f, 1);

      if (Mathf.Abs(inputX) + Mathf.Abs(inputY) < .01 && Value < 1)
         Value = Value + Time.deltaTime;
	}
}
