using System;
using System.Collections;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine;

public class Resurrect : MonoBehaviour
{
    private bool _isAlive = true;
    [SerializeField] private float ResurrectTimer;


    public void Die()
    {
        if (!_isAlive) return;
        _isAlive = false;
        PlayDeathAnimation();
    }

    IEnumerator ResurrectAfterTime()
    {
        yield return new WaitForSeconds(ResurrectTimer);
        Respawn();
    }

    public void Respawn()
    {
        if (_isAlive) return;

        _isAlive = true;
        PlayResurrectAnimation();
    }

    private void PlayResurrectAnimation()
    {
        gameObject.SetActive(true);

        _spriteRenderer.DOColor(Color.white, 0.5f).SetEase(Ease.InQuad)
            .OnComplete(() => { _collider2D.enabled = true; });
    }

    private void PlayDeathAnimation()
    {
        _isAlive = false;
        CameraShake.ShakeCamera(0.5f, 20f);
        _collider2D.enabled = false;
        transform.DOPunchScale(new Vector3(1.2f, 0.8f, 1f), 0.3f, 5, 1F)
            .OnComplete(() =>
            {
                _spriteRenderer.DOColor(_spriteRenderer.color/10f,0.1f);
                if (ResurrectTimer != 0) StartCoroutine(ResurrectAfterTime());
            });
    }

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Collider2D _collider2D;

    public bool IsAlive()
    {
        return _isAlive;
    }
}