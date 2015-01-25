#pragma strict

var handJoystick : Joystick;

function Start () {

}

function FixedUpdate ()
{
  var inputX = JoyStickInput(handJoystick).x;
  var inputY = JoyStickInput(handJoystick).y;
  var handPosition = new Vector2(inputX, inputY);
  var minimalHandMagnitude = .2f;
  GetComponent(SpriteRenderer).enabled = handPosition.sqrMagnitude > minimalHandMagnitude;
  SetAplha(GetComponent(SpriteRenderer).material, handPosition.sqrMagnitude);
  transform.localPosition = handPosition * .5f;
}

function SetAplha(material : Material, value : float){
  var color = material.color;
  color.a = value;
  material.color = color;
}

function JoyStickInput(joystick : Joystick)
{
  return Vector2(joystick.position.x, joystick.position.y);
}
