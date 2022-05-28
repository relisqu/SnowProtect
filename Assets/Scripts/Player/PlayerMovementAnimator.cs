using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayerMovementAnimator : MonoBehaviour
{
        public static bool IsSliding;

        [SerializeField] private Color RedColor;
        private void Start()
        {
                _previousPosition = transform.position;
                _animator = GetComponent<Animator>();

        }

        public void ShowPlayerDamage(PlayerMovement player)
        {
                player.GetSpriteRenderer().DOColor(RedColor, 0.2f).OnComplete(() =>
                {
                        player.GetSpriteRenderer().DOColor(Color.white, 0.2f).SetEase(Ease.InSine);
                });
        }

        private void FixedUpdate()
        {
                var direction = (transform.position - _previousPosition).normalized;
               

                var magnitude = (_previousPosition - transform.position).sqrMagnitude;
                var x = Mathf.Clamp01(Math.Abs(direction.x ));
                var y = Mathf.Sign(direction.y)*Mathf.Clamp01(Math.Abs(direction.y ));
                
                _animator.SetBool(IsRunning,magnitude>0.001f); 
                _animator.SetBool(Sliding,IsSliding); 
                if (Vector2.Dot(transform.position, _previousPosition) < 0.08f || magnitude<0.001f)
                {
                        _previousPosition = transform.position;
                        return;
                        
                }
                _animator.SetFloat(MovementY,y);
                _animator.SetFloat(MovementX,x); 
                _direction = direction;
                _previousPosition = transform.position;
        }
        
        
        private Vector3 _previousPosition;
        private Vector2 _direction;
        private Animator _animator;
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        private static readonly int MovementY = Animator.StringToHash("MovementY");
        private static readonly int MovementX = Animator.StringToHash("MovementX");
        private static readonly int Sliding = Animator.StringToHash("IsSliding");

        public Vector2 GetMovement()
        {
                return _direction;
        }
}