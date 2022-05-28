using System;
using DefaultNamespace;
using UnityEngine;

namespace Surfaces
{
    public abstract class Surface : MonoBehaviour
    {
        [SerializeField] private float SnowMeltCoefficient;

        void EnterSurface(GameObject other)
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

        void StayAtSurface(GameObject other)
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

        void ExitSurface(GameObject other)
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

        private void OnTriggerStay2D(Collider2D other)
        {
            StayAtSurface(other.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            EnterSurface(other.gameObject);
        }


        private void OnTriggerExit2D(Collider2D other)
        {
            ExitSurface(other.gameObject);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            EnterSurface(other.gameObject);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            ExitSurface(other.gameObject);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            StayAtSurface(other.gameObject);
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