using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : Shootable
{
    [Header("레이저 인생시간")]
    [SerializeField]
    protected float lazerLifetime;

    private Animator anim;

    public float LazerLifetime
    {
        get
        {
            return lazerLifetime;
        }
        set
        {
            lazerLifetime = value;
        }
    }

    // 레이져 크기
    [Header("레이저 크기")]
    [SerializeField]
    private Vector2 lazerSize;

    public Vector2 LazerSize 
    { 
        get 
        {
            return lazerSize;
        } 
        set 
        {
            lazerSize = value;
        } 
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        StartCoroutine(Lifetime());
    }

    public override void InsertQueue()
    {
        //인서트 큐 하세요
    }

    private IEnumerator Lifetime()
    {
        yield return null;
        transform.localScale = new Vector3(LazerSize.x, LazerSize.y);
        yield return new WaitForSeconds(lazerLifetime - 0.8f);
        anim.SetTrigger("OnEnd");
        yield return new WaitForSeconds(0.5f);
        InsertQueue();
    }

    private void OnDisable()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}