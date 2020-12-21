using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateLazerAttack : MonoBehaviour, IState
{
    [HideInInspector]
    public Lazer lazer;
    [HideInInspector]
    public Vector2 lazerSize = new Vector2(0.45f, 8f);
    [HideInInspector]
    public float lazerLifetime = 0.5f;
    [HideInInspector]
    public Vector3 lazerRotation = new Vector3(0f, 0f, -180f);
    [HideInInspector]
    public Transform attackPos;

    public float LazerLifetime { get; set; }

    public void OperateEnter()
    {
        lazer = PoolManager.Instance.GetQueue(PoolManager.LazerType.RED).GetComponent<Lazer>();
        lazer.transform.localPosition = attackPos.position;
        lazer.transform.Rotate(lazerRotation, Space.Self);
        lazer.LazerLifetime = lazerLifetime;
        lazer.LazerSize = lazerSize;
    }

    public void OperateUpdate()
    {

    }

    public void OperateExit()
    {

    }
}
