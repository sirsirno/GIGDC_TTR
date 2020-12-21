using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StateSideMove : MonoBehaviour, IState
{
    private Vector3 moveDir = Vector3.zero;
    private int randX = 0;
    private int randY = 0;

    private bool is_Turn = false;
    public float turnValue;

    private int temp = 0;
    public float movSpeed;

    private int maxMove;
    private int moveCount;

    private void Start()
    {
        maxMove = Random.Range(4, 7);
    }

    public void OperateEnter()
    {
        if (moveCount <= maxMove) // 카운트까지 랜덤 y
        {
            do
            {
                randY = (int)Random.Range(GameManager.Instance.MapOrigin.y, GameManager.Instance.MapOrigin.y + GameManager.Instance.MapInfo.y);
            } while (temp.Equals(randY));
            temp = randY;
            moveDir.x = transform.position.x;
            moveDir.y = randY;

            transform.DOMove(moveDir, movSpeed);

            moveCount++;
        }
        else
        {
            if (!is_Turn) // 한번 돌아주는거
            {
                is_Turn = true;

                transform.DORotate(new Vector3(0f, 0f, 0f), movSpeed);
                this.GetComponent<Enemy>().SetAttackDir(AttackDir.DOWN);

                randX = (int)Random.Range(GameManager.Instance.MapOrigin.x - 1, GameManager.Instance.MapOrigin.x + GameManager.Instance.MapInfo.x);
                temp = randX;
                moveDir.x = randX;
                moveDir.y = GameManager.Instance.MapOrigin.y + 8f;

                transform.DOMove(moveDir, movSpeed);

                return;
            }

            do
            {
                randX = (int)Random.Range(GameManager.Instance.MapOrigin.x - 1, GameManager.Instance.MapOrigin.x + GameManager.Instance.MapInfo.x);
            } while (temp.Equals(randX));
            temp = randX;
            moveDir.x = randX;
            moveDir.y = transform.position.y;

            transform.DOMove(moveDir, movSpeed);
        }
    }

    public void OperateUpdate()
    {

    }

    public void OperateExit()
    {

    }
}
