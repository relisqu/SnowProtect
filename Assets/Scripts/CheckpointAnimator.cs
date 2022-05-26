using System;
using DG.Tweening;
using UnityEngine;

public class CheckpointAnimator : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _renderer;
    [SerializeField] private Material DefaultMat;


    private void Start()
    {
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void PlayWinAnimation()
    {
        transform.DOScaleY(0.5f, 0.1f).SetEase(Ease.Linear).OnComplete(() =>
        {
            transform.DOScaleY(1f, 0.2f).SetEase(Ease.OutBounce);
        });
    }

    public void RecolorToMarkedColor()
    {
        _renderer.material = DefaultMat;
    }
}