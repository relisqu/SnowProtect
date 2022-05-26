using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class PlayerInput : MonoBehaviour
{
    public static Vector2 MovementInput;    
    [SerializeField]private InputActionAsset MoveAction;
    [SerializeField]private float InteractionRange;
    [SerializeField]private PlayerPush PlayerPush;

    private void Update()
    {
        MovementInput = MoveAction["Player/Move"].ReadValue<Vector2>();
        if (_isPushing)
        {
            PlayerPush.PreparePush();
        }
        else
        {
            PlayerPush.RegainPush();
        }
    }
    public void Interact(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        int maxColliders = 3;
        var hitColliders = new Collider2D[maxColliders];
        var size = Physics2D.OverlapCircleNonAlloc(transform.position, InteractionRange, hitColliders);
        if(size==0) return;
        foreach (var collider in hitColliders)
        {
            if (collider!=null && collider.TryGetComponent(out Interactable item))
            {
                item.Interact(this);
            }
        }
    }
    
    private bool _isPreparingToPush;
    public void Push(InputAction.CallbackContext context)
    {
        if (!context.canceled && !_isPreparingToPush) return;
        PlayerPush.PushSnowball();
        _isPreparingToPush = false;

    }

    private bool _isPushing;
    public void PrepareToPush(InputAction.CallbackContext context)
    {
        if (context.canceled) _isPushing = false;
        if (!context.performed) return;
        _isPreparingToPush = true;
        _isPushing = true;

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,InteractionRange);
    }
}