using System.Collections;
using System.Numerics;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float BombPower;
    [SerializeField] private Resurrect Health;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!Health.IsAlive()) return;

        if (other.gameObject.TryGetComponent(out Entity ball))
        {
            if (ball.GetType() == typeof(PlayerMovement))
            {
                var player = (PlayerMovement) ball;
                player.SetMovementState(PlayerMovement.State.PHYSIC_AFFECTED);
                player.ShowDamage();
                StartCoroutine(ChangePlayerState(player));
                DamageEntity(ball, BombPower);
            }

            if (ball.GetType() == typeof(Snowball))
            {
                var snowball = (Snowball) ball;
                snowball.TakeDamage(snowball.getCurrentLifetime() / 5);
                StartCoroutine(TrunkEntityVelocity(ball));
                DamageEntity(ball, BombPower * snowball.GetDefaultDrag()/8f);
            }
            print(ball.GetMovementSpeed());
            Health.Die();
        }
    }

    IEnumerator TrunkEntityVelocity(Entity entity)
    {
        yield return new WaitForSeconds(1f);
        entity.ClampVelocity(20f);
    }

    IEnumerator ChangePlayerState(PlayerMovement player)
    {
        yield return new WaitForSeconds(1f);
        if (player.GetMovingState() == PlayerMovement.State.PHYSIC_AFFECTED)
            player.SetMovementState(PlayerMovement.State.MOVING);
    }

    void DamageEntity(Entity entity, float force)
    {
        entity.AddForce(entity.transform.position - transform.position, force);
        
        entity.transform.DOPunchScale(Vector3.one * .7f, 0.5f, 3, 1F).OnComplete(() =>
        {
            entity.transform.localScale = Vector3.one;
        });
    }

}