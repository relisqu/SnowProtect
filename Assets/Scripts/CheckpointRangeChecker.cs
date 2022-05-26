using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class CheckpointRangeChecker : MonoBehaviour, Interactable
    {
        private Checkpoint _checkpoint;
        [SerializeField]private float Range;

        private void Start()
        {
            _checkpoint = GetComponent<Checkpoint>();
        }

        public void Interact(PlayerInput playerMovement)
        {
            _checkpoint.GenerateNewSnowball();
        }
    }
}