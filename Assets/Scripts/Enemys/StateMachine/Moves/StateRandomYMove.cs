using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StateRandomYMove : MonoBehaviour
{
    private Vector3 moveDir = Vector3.zero;
    private int randY = 0;
    private int temp = 0;
    public float movSpeed = 0f;

    public void OperateEnter()
    {
        do
        {
            randY = (int)Random.Range(GameManager.Instance.MapOrigin.y, GameManager.Instance.MapOrigin.y + GameManager.Instance.MapInfo.y);
        } while (temp.Equals(randY));
        temp = randY;
        moveDir.x = transform.position.y;
        moveDir.y = randY;

        transform.DOMove(moveDir, movSpeed);
    }

    public void OperateUpdate()
    {

    }

    public void OperateExit()
    {

    }           
}
