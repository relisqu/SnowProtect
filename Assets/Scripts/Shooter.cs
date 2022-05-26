using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private DieOnTouch FireObject;
    [SerializeField] private float PoolSize;
    [SerializeField] private float PausesBetweenShoots;
    [SerializeField] private bool IsActive;
    [SerializeField] private Transform RootTransform;
    [SerializeField] private List<DieOnTouch> ObjectPool;


    private void Start()
    {
        for (int i = 0; i < PoolSize; i++)
        {
            var newFireball = Instantiate(FireObject, RootTransform.position, RootTransform.rotation, RootTransform);
            ObjectPool.Add(newFireball);
            newFireball.gameObject.SetActive(false);
        }

        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            if (!IsActive) yield return null;
            transform.DOPunchScale(new Vector3(0.3f,0.4f,1f), 0.2f, 10, 1F);
            yield return new WaitForSeconds(0.1f);
            FindObjectFromPool();
            yield return new WaitForSeconds(PausesBetweenShoots);
        }

        yield return null;
    }

    DieOnTouch FindObjectFromPool()
    {
        foreach (var fireball in ObjectPool)
        {
            if (!fireball.gameObject.activeInHierarchy)
            {
                fireball.transform.position = RootTransform.position;
                fireball.transform.rotation = RootTransform.rotation;
                fireball.gameObject.SetActive(true);
                fireball.ResetParameters();
                return fireball;
            }
        }

        return null;
    }
}