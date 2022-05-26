using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : Entity
{
    [SerializeField] private float Speed;
    [SerializeField] private float MaximumSpeed;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _playerMovementAnimator = GetComponent<PlayerMovementAnimator>();
        _defaultSpeed = Speed;
        _defaultDrag = _rigidbody.drag;
    }

    void Update()
    {
        if (MovingState == State.JUMPING) return;

        var velocity = _rigidbody.velocity;
        var currentSpeed = Speed * _speedMultiplier;
        _spriteRenderer.flipX = velocity.x < 0;
        var input = PlayerInput.MovementInput;
        _rigidbody.AddForce(input * (Time.deltaTime * currentSpeed), ForceMode2D.Impulse);
        if (MovingState != State.PHYSIC_AFFECTED)
            _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, MaximumSpeed);
    }

    public void ChangeMovementDrag(float slipperyCoefficient, float slipperyBoost)
    {
        ChangeMovementToDefault();
        _rigidbody.drag /= slipperyCoefficient;
        Speed /= slipperyCoefficient;
        Speed += slipperyBoost;
    }

    public void ChangeMovementToDefault()
    {
        _rigidbody.drag = _defaultDrag;
        Speed = _defaultSpeed;
    }


    public void SetMovementSpeedMultiplier(float f)
    {
        _speedMultiplier = f;
    }

    public SpriteRenderer GetSpriteRenderer()
    {
        return _spriteRenderer;
    }

    public void SetMovementState(State state)
    {
        MovingState = state;
    }

    public void TakeDamage(Vector3 direction, float verticalForce, float horizontalForce)
    {
        MovingState = State.JUMPING;
        var jumpDir = (direction.normalized * horizontalForce).x;
        ShowDamage();
        transform.DOLocalJump(transform.position + new Vector3(jumpDir, 0, 0), verticalForce, 1, 0.8f, false);
        
        MovingState = State.MOVING;
    }

    public enum State
    {
        MOVING,
        JUMPING,
        PHYSIC_AFFECTED
    };

    private float _speedMultiplier = 1;
    private SpriteRenderer _spriteRenderer;
    private float _defaultSpeed;
    private float _defaultDrag;
    private State MovingState;
    private PlayerMovementAnimator _playerMovementAnimator;

    public State GetMovingState()
    {
        return MovingState;
    }

    public void ShowDamage()
    {
        _playerMovementAnimator.ShowPlayerDamage(this);
    }
}