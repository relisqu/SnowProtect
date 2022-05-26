using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class ForwardLinearMovement : MonoBehaviour
    {
        [SerializeField] private float Speed;
        private bool _isMoving = true;

        private void Update()
        {
            if (_isMoving) transform.Translate(Vector3.down * (Speed * Time.deltaTime));
        }

        public void Stop()
        {
            _isMoving = false;
        }  
        public void Play()
        {
            _isMoving = true;
        }
    }
}