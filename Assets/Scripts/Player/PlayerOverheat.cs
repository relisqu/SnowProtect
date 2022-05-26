using System;
using System.Numerics;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Surfaces;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

namespace Player
{
    public class PlayerOverheat : MonoBehaviour
    {
        [SerializeField] private float MaximumOverheat;
        [SerializeField] private float SpeedOfHeating;
        [SerializeField] private Color RedColor;
        [SerializeField] private PlayerMovement PlayerMovement;
        [SerializeField] private float DecreaseSpeed;

        private float _overheatTimer;
        private SpriteRenderer _spriteRenderer;
        private bool _isOnLava;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void ChangeLavaTimer(float offset)
        {
            _overheatTimer += offset;
            if (_overheatTimer < MaximumOverheat) return;
            PlayerMovement.TakeDamage(Random.insideUnitCircle, 2f,  6f);
            _overheatTimer /= 6;
        }

        private void Update()
        {
            _spriteRenderer.color = Color.Lerp(Color.white, RedColor, _overheatTimer / MaximumOverheat);
        }

        private Lava _lava;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Lava lava))
            {
                _lava = lava;
                seq?.Kill();
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.TryGetComponent(out Lava lava))
            {
                if (_lava != null)
                {
                    ChangeLavaTimer(Time.deltaTime*2);
                    
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Lava lava))
            {
                if (_lava == lava)
                {
                    _lava = null;
                    seq= DOTween.To(()=> _overheatTimer, x=> _overheatTimer = x, 0,DecreaseSpeed).SetSpeedBased();
                   
                }
            }
        }

        private TweenerCore<float, float, FloatOptions> seq;
    }
}