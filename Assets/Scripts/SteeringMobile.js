#pragma strict

var moveJoystick : Joystick;

function Start () {

}

function FixedUpdate ()
{
  var inputX = JoyStickInput(moveJoystick).x;
  var inputY = JoyStickInput(moveJoystick).y;
  var inputAndSpeedSignsAreSameForX = !(GetComponent.<Rigidbody2D>().velocity.x * inputX < 0);
  var inputAndSpeedSignsAreSameForY = !(GetComponent.<Rigidbody2D>().velocity.y * inputY < 0);
  var x = Mathf.Abs(GetComponent.<Rigidbody2D>().velocity.x) > 3 && inputAndSpeedSignsAreSameForX ? 0 : inputX;
  var y = Mathf.Abs(GetComponent.<Rigidbody2D>().velocity.y) > 3 && inputAndSpeedSignsAreSameForY ? 0 : inputY;
  var forceToAdd = Vector2(x, y) * 240 * Time.fixedDeltaTime * GetComponent.<Rigidbody2D>().mass;
  GetComponent.<Rigidbody2D>().AddForce(forceToAdd);
}

function JoyStickInput(joystick : Joystick)
{
  return Vector2(joystick.position.x, joystick.position.y);
}
