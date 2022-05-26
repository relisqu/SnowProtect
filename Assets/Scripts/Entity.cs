using UnityEngine;

namespace DefaultNamespace
{
    public abstract class Entity : MonoBehaviour
    {
        protected Rigidbody2D _rigidbody;

        public void AddForce(Vector2 getMovement, float force)
        {
            _rigidbody.AddForce(getMovement.normalized * force, ForceMode2D.Impulse);
        }

        public float GetMovementSpeed()
        {
            return _rigidbody.velocity.magnitude;
        }

        public void ClampVelocity(float f)
        {
            _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, f);
        }
    }
}