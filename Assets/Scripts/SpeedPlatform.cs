using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class SpeedPlatform : MonoBehaviour
    {
        
        [SerializeField] private float SpeedMultiplier;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerMovement player))
            {
                player.SetMovementSpeedMultiplier( SpeedMultiplier);
            } 
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerMovement player))
            {
                player.SetMovementSpeedMultiplier( SpeedMultiplier);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerMovement player))
            {
                player.SetMovementSpeedMultiplier(1f);
            }
        }
    }
}