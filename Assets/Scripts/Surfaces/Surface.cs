using DefaultNamespace;
using UnityEngine;

namespace Surfaces
{
    public abstract class Surface : MonoBehaviour
    {
        [SerializeField] private float SnowMeltCoefficient;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerMovement player))
            {
                EnterSurface(player);
            }
            else if (other.TryGetComponent(out Snowball snowball))
            {
                snowball.SetMeltMultiplier(SnowMeltCoefficient);
                EnterSurface(snowball);
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerMovement player))
            {
                StayAtSurface(player);
            }
            else if (other.TryGetComponent(out Snowball snowball))
            {
                snowball.SetMeltMultiplier(SnowMeltCoefficient);
                StayAtSurface(snowball);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerMovement player))
            {
                ExitSurface(player);
            }
            else if (other.TryGetComponent(out Snowball snowball))
            {
                snowball.SetDefaultMeltMultiplier();
                ExitSurface(snowball);
            }
        }

        protected virtual void EnterSurface(PlayerMovement player)
        {
        }

        protected virtual void StayAtSurface(PlayerMovement player)
        {
        }

        protected virtual void ExitSurface(PlayerMovement player)
        {
        }

        protected virtual void EnterSurface(Snowball snowball)
        {
        }

        protected virtual void StayAtSurface(Snowball snowball)
        {
        }

        protected virtual void ExitSurface(Snowball snowball)
        {
        }
    }
}