using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Checkpoint : MonoBehaviour
    {
        [SerializeField] private Transform SnowSpawnPoint;
        [SerializeField] private Snowball SnowballObject;
        private PlayerMovement player;
        private CheckpointAnimator _animator;
        private bool playerPassed;


        private void Start()
        {
            player = FindObjectOfType<PlayerMovement>();
            _animator = GetComponent<CheckpointAnimator>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Snowball snowball))
            {
                if (playerPassed)
                {
                }
                else
                {
                    MarkAsCheckpoint();
                }

                print("Nice");
                snowball.Heal();
            }
        }

        private void MarkAsCheckpoint()
        {
            playerPassed = true;
            _animator.PlayWinAnimation();
            _animator.RecolorToMarkedColor();
        }

        public void GenerateNewSnowball()
        {
            if (Snowball.SnowballsCount <= 0 && playerPassed)
            {
                Instantiate(SnowballObject, SnowSpawnPoint.position, Quaternion.identity);
                _animator.PlayWinAnimation();
            }
        }
    }
}