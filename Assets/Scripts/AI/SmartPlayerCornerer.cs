using UnityEngine;

namespace Assets.Scripts.AI
{
    public class SmartPlayerCornerer : SmartPlayerChaser
    {

        protected override Vector2 Seek(Vector2 targetPosition)
        {
            Vector2 toPlayer = targetPosition - GetComponent<Rigidbody2D>().position;
            float roundingAngle = 35f;
            Vector2 roundingToPlayer = Quaternion.AngleAxis(roundingAngle, Vector3.forward) * toPlayer;

            var desiredVelocity = (roundingToPlayer).normalized * MaxVelocitySqr;
            return desiredVelocity - GetComponent<Rigidbody2D>().velocity;
        }
    }
}