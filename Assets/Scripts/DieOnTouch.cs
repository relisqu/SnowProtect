using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace DefaultNamespace
{
    public class DieOnTouch : MonoBehaviour
    {
        [SerializeField] private string DieAnimation;
        [SerializeField] private Animator Animator;
        [SerializeField] private Transform FireTransform;
        [SerializeField] private ParticleSystem TrailParticleSystem;
        private bool _isAlive = true;
        private Material _material;
        private float _defaultIntensity;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            _forwardLinearMovement = GetComponent<ForwardLinearMovement>();
            _collider = GetComponent<Collider2D>();
            _light2D = GetComponentInChildren<Light2D>();
            _defaultIntensity = _light2D.intensity;
            _material = _particleSystem.GetComponent<ParticleSystemRenderer>().material;
            
        }

        private IEnumerator DieAfterTime()
        {
            yield return new WaitForSeconds(15f);
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_isAlive) return;
            if (other.gameObject.TryGetComponent(out Snowball ball))
            {
                ball.AddForce(ball.transform.position - transform.position, 5f);
                PlayDeathAnimation();
            }
            else if (other.TryGetComponent(out PlayerMovement player))
            {
                player.TakeDamage(player.transform.position - transform.position, 2f, 5f);

                PlayDeathAnimation();
            }
        }

        public void ResetParameters()
        {
            _isAlive = true;

            TrailParticleSystem.Play();
            TrailParticleSystem.enableEmission = true;
            _forwardLinearMovement.Play();
            _collider.enabled = true;
            _material.color /= 0.9f;
            _light2D.intensity = _defaultIntensity;
            FireTransform.localScale = Vector3.one;
            StartCoroutine(DieAfterTime());
        }

        public void PlayDeathAnimation()
        {
            if (!_isAlive) return;
            TrailParticleSystem.Stop();
            TrailParticleSystem.enableEmission = false;
            CameraShake.ShakeCamera(0.15f, 6f);
            _forwardLinearMovement.Stop();
            _collider.enabled = false;
            _particleSystem.Play();
            var originalColor = _material.color;
            _material.DOColor(originalColor * 0.9f, 0.9f).SetEase(Ease.InSine);
            var intensity = _light2D.intensity;


            DOTween.To(() => intensity, x => intensity = x, 0f, 1).OnUpdate(() => { _light2D.intensity = intensity; });

            FireTransform.DOScale(Vector3.zero, 0.4f).SetEase(Ease.OutSine).OnComplete(() =>
            {
                StartCoroutine(Die());
            });
            _isAlive = false;
            if (DieAnimation != null && Animator != null)
            {
                Animator.SetTrigger(DieAnimation);
            }
        }

        public IEnumerator Die()
        {
            // Destroy(gameObject, 2f);
            yield return new WaitForSeconds(2f);
            gameObject.SetActive(false);
        }

        private ParticleSystem _particleSystem;
        private ForwardLinearMovement _forwardLinearMovement;
        private Collider2D _collider;
        private Light2D _light2D;
    }
}