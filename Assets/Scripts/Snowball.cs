using DG.Tweening;
using Surfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class Snowball : Entity, Interactable
    {
        [SerializeField] private float Lifetime;
        [SerializeField] private float MaximumLifetime;
        [SerializeField] private float MaxSnowballSize;
        [SerializeField] private float Drag;
        [SerializeField] private float PickupDistance;

        public static int SnowballsCount;

        private void Start()
        {
            _initialScale = transform.localScale.x;
            _currentLifetime = Lifetime;
            _rigidbody = GetComponent<Rigidbody2D>();
            _renderer = GetComponentInChildren<SpriteRenderer>();
            _player = FindObjectOfType<SnowPickup>();
            _defaultLayer = _renderer.sortingOrder;
            SnowballsCount++;
            ChangeDrag(GetDefaultDrag());
        }

        public void ChangeSortingLayer(int shift)
        {
            _renderer.sortingOrder = _defaultLayer + shift;
        }

        private void Update()
        {
            _currentLifetime = Mathf.Min(_currentLifetime, MaximumLifetime);
            _currentLifetime -= Time.deltaTime * _meltMultiplier;
            if (_currentLifetime <= 0.2f || transform.localScale.x <= 0.3f) Die();
            transform.localScale = Mathf.Lerp(0.3f, MaxSnowballSize, (_currentLifetime / MaximumLifetime)) *
                                   _initialScale * Vector3.one;


            if (_isPicked)
            {
                DisableCollider(true);
                return;
            }

            DisableCollider(false);
        }

        public void ChangeDrag(float surfaceDrag)
        {
            _rigidbody.drag = surfaceDrag;
        }

        private void Die()
        {
            Destroy(gameObject);
            SnowballsCount--;
        }


        public void AddLifetime(float d)
        {
            _currentLifetime += d;
        }

        public void SetMeltMultiplier(float melt)
        {
            _meltMultiplier = melt;
            print("Set multiplier to: " + _meltMultiplier);
        }


        public void SetPicked(bool b)
        {
            _isPicked = b;
        }


        public void Heal()
        {
            if (_currentLifetime < Lifetime) _currentLifetime = Lifetime;
        }

        public void SetDefaultMeltMultiplier()
        {
            _meltMultiplier = 1;
        }


        public float GetDefaultDrag()
        {
            return Drag;
        }

        public void Interact(PlayerInput playerMovement)
        {
            if (!(Vector3.Distance(transform.position, playerMovement.transform.position) <= PickupDistance)) return;

            if (_isPicked)
            {
                _player.DropSnowball(this);
                _isPicked = false;
            }
            else
            {
                _player.PickSnowball(this);
                _isPicked = true;
            }
        }


        public void TakeDamage(float amount)
        {
            _currentLifetime -= amount;
            transform.DOPunchScale(Vector3.one * .5f, 0.3f, 7, 1F);
        }

        public float getCurrentLifetime()
        {
            return _currentLifetime;
        }
        private void DisableCollider(bool isDisable)
        {
            _rigidbody.isKinematic = isDisable;
            Physics2D.IgnoreLayerCollision(6, 7, isDisable);
            Physics2D.IgnoreLayerCollision(6, 0, isDisable);
            Physics2D.IgnoreLayerCollision(6, 2, isDisable);
        }

        private float _currentLifetime;
        private float _initialScale;
        private float _meltMultiplier = 1f;
        private int _defaultLayer;
        private bool _isPicked;
        private bool _isOnIce;
        private SnowPickup _player;
        private SpriteRenderer _renderer;
    }
}