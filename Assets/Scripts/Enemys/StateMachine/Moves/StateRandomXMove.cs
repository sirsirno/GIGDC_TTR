using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StateRandomXMove : MonoBehaviour, IState
{
    private Vector3 moveDir = Vector3.zero;
    private int randX = 0;
    private int temp = 0;
    public float movSpeed = 0f;

    public void OperateEnter()
    {
        do {
            randX = (int)Random.Range(GameManager.Instance.MapOrigin.x - 1, GameManager.Instance.MapOrigin.x + GameManager.Instance.MapInfo.x);
        } while (temp.Equals(randX));
        temp = randX;
        moveDir.x = randX;
        moveDir.y = transform.position.y;

        transform.DOMove(moveDir, movSpeed);
    }

    public void OperateUpdate()
    {

    }

    public void OperateExit()
    {

    }
}
