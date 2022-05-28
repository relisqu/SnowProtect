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
        [SerializeField] private PlayerMovement Player;
        [SerializeField] private float PushSpeed = 1;
        [SerializeField] private float MaxPushCharge;
        [SerializeField] private ParticleSystem ParticleSystem;

        [SerializeField] private float Force;

        private void Update()
        {
            var movement = Movement.GetMovement();
            ZoneTransform.rotation = Quaternion.LookRotation(Vector3.forward, -movement);
        }

        public bool CanPush => _pickedSnowball != null;

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

        public bool PushSnowball()
        {
            if (_pickedSnowball == null) return false;

            _pickedSnowball.AddForce(Movement.GetMovement(), Force * _currentPushCharge / MaxPushCharge);
            EmitParticles();
            CameraShake.ShakeCamera(0.1f, 2 * _currentPushCharge / (MaxPushCharge));
            ParentTransform.DOScale(new Vector3(0.9f, 1.2f + (_currentPushCharge / (2 * MaxPushCharge)), 1), 0.2f)
                .OnComplete(() => { ParentTransform.DOScale(new Vector3(1f, 1, 1), 0.1f).SetEase(Ease.OutBounce); });

            Player.SetMovementSpeedMultiplier(1f);
            _currentPushCharge = 0;
            return true;
        }

        private void EmitParticles()
        {
            if (_currentPushCharge / MaxPushCharge > 0.8f)
            {
                ParticleSystem.Play();
            }
            else
            {
                ParticleSystem.Emit((int)(_currentPushCharge / MaxPushCharge)* 15);
            }
        }

        public void PreparePush()
        {
            _currentPushCharge += Time.deltaTime * PushSpeed;
            _currentPushCharge = Mathf.Clamp(_currentPushCharge, 0, MaxPushCharge);
            ChangePlayerScale();
            ClampPlayerSpeed();
        }

        public void RegainPush()
        {
            _currentPushCharge -= Time.deltaTime * 2 * PushSpeed;
            _currentPushCharge = Mathf.Clamp(_currentPushCharge, 0, MaxPushCharge);
            ChangePlayerScale();
            if (_currentPushCharge == 0)
            {
                Player.SetMovementSpeedMultiplier(1f);
                return;
            }

            ClampPlayerSpeed();
        }

        private void ClampPlayerSpeed()
        {
            Player.SetMovementSpeedMultiplier(Mathf.Clamp01(1.35f - _currentPushCharge / MaxPushCharge));
        }

        private void ChangePlayerScale()
        {
            ParentTransform.localScale =
                Vector3.Lerp(Vector3.one, new Vector3(1.1f, 0.6f, 1), _currentPushCharge / MaxPushCharge);
        }

        private float _currentPushCharge;
        private Snowball _pickedSnowball;
    }
}