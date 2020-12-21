using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StateMoveCenter : MonoBehaviour, IState
{
    public float MovSpeed { private get; set; }
    private Vector3 moveDir = Vector3.zero;

    public void OperateEnter()
    {
        moveDir.x = GameManager.Instance.MapCenter.x;
        moveDir.y = transform.position.y;

        transform.DOMove(moveDir, MovSpeed);
    }

    public void OperateUpdate()
    {

    }

    public void OperateExit()
    {

    }
}
