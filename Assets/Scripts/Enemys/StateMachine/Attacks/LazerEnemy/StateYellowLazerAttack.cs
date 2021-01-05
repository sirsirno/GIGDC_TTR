using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateYellowLazerAttack : MonoBehaviour, IState
{
    [HideInInspector]
    public Lazer lazer { get; set; }
    [HideInInspector]
    public Vector2 lazerSize = new Vector2(0.45f, 8f);
    [HideInInspector]
    public float lazerLifetime = 0.5f;
    [HideInInspector]
    public Vector3 lazerRotation { get; set; } = new Vector3(0f, 0f, -180f);
    [HideInInspector]
    public Transform attackPos { get; set; }
    [HideInInspector]
    public float damage { get; set; } = 20f;

    public float LazerLifetime { get; set; }

    public void OperateEnter()
    {
        lazer = PoolManager.Instance.GetQueue(PoolManager.LazerType.YELLOW).GetComponent<Lazer>();
        lazer.transform.localPosition = attackPos.position;
        lazer.transform.Rotate(lazerRotation, Space.Self);
        lazer.LazerLifetime = lazerLifetime;
        lazer.LazerSize = lazerSize;
        lazer.Damage = damage;
    }

    public void OperateUpdate()
    {

    }

    public void OperateExit()
    {

    }
}
