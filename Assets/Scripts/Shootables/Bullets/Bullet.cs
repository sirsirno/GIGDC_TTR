using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : Shootable
{
    [SerializeField]
    protected float bulletLifetime;

    [SerializeField]
    protected float bulletSpeed;

    private void OnEnable()
    {
        StartCoroutine(Lifetime());
        EventManager.Instance.OnInsertBullet += InsertQueue;
    }

    protected virtual void Update()
    {
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime, Space.Self);
    }

    public override void InsertQueue()
    {
        //인서트 큐 하세요
    }

    private IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(bulletLifetime);
        InsertQueue();
    }

    private void OnDisable()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.OnInsertBullet -= InsertQueue;
        }
    }
}
