using System;
using System.Numerics;
using DG.Tweening;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace DefaultNamespace
{
    public class PlayerPush : MonoBehaviour
    {
        [SerializeField] private Transform ZoneTransform;
        [SerializeField] private Transform ParentTransform;
        [SerializeField] private PlayerMovementAnimator Movement;
        private Snowball _pickedSnowball;
        [SerializeField] private float Force;

        private void Update()
        {
            var movement = Movement.GetMovement();
            ZoneTransform.rotation = Quaternion.LookRotation(Vector3.forward, -movement);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Snowball ball))
            {
                _pickedSnowball = ball;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Snowball ball))
            {
                if (ball == _pickedSnowball) _pickedSnowball = null;
            }
        }

        private float _currentPushCharge;
        [SerializeField]private float PushSpeed=1;
        [SerializeField]private float MaxPushCharge;
        void ChargePush()
        {
            _currentPushCharge = 0;
            
        }

        public void PushSnowball()
        { /*
            ParentTransform.DOScale(new Vector3(1.2f, 0.8f, 1), 0.2f).OnComplete(() =>
            {
                ParentTransform.DOScale(new Vector3(0.9f, 1.3f, 1), 0.2f).OnComplete(() =>
                {
                    if (_pickedSnowball != null)
                    {
                        _pickedSnowball.AddForce(Movement.GetMovement(), Force);
                    }

                    ParentTransform.DOScale(new Vector3(1f, 1, 1), 0.1f).SetEase(Ease.OutBounce);
                });
            });*/
        }

        public void PreparePush()
        {
            _currentPushCharge += Time.deltaTime * PushSpeed;
            _currentPushCharge = Mathf.Clamp(_currentPushCharge, 0, MaxPushCharge);
            ParentTransform.localScale =
                Vector3.Lerp(Vector3.one, new Vector3(1.2f, 0.6f, 1), _currentPushCharge / MaxPushCharge);
        }

        public void RegainPush()
        { 
            _currentPushCharge -= Time.deltaTime * 2*PushSpeed;
            _currentPushCharge = Mathf.Clamp(_currentPushCharge, 0, MaxPushCharge);
            ParentTransform.localScale =
                Vector3.Lerp(Vector3.one, new Vector3(1.2f, 0.6f, 1), _currentPushCharge / MaxPushCharge);
        }
    }
}