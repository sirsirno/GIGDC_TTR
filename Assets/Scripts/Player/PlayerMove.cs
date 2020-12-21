using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float moveDur = 1f;
    private float x = 0f;
    private float y = 0f;
    private bool is_Move = false;

    [SerializeField]
    private LayerMask groundMask;

    private Vector3 dir = Vector3.zero;

    private GroundScript nextGround;
    private Collider2D groundColl;

    private void OnEnable()
    {
        StartCoroutine(Move());
    }

    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
    }

    private IEnumerator Move()
    {
        while (true)
        {
            if (y > 0)
            {
                dir = Vector3.up;
            }
            else if (x < 0)
            {
                dir = Vector3.left;
            }
            else if (y < 0)
            {
                dir = Vector3.down;
            }
            else if (x > 0)
            {
                dir = Vector3.right;
            }

            if (!is_Move && (!x.Equals(0) || !y.Equals(0)))
            {
                if (GetNextGround())
                {
                    is_Move = true;

                    switch (nextGround.groundType)
                    {
                        case GroundType.NORMAL:
                            transform.DOMove(transform.position + dir, moveDur).OnComplete(() => { is_Move = false; });
                            break;

                        case GroundType.ICE:

                            do {
                                transform.DOMove(transform.position + dir, moveDur).OnComplete(() => { is_Move = false; });
                                yield return new WaitUntil(() => { return !is_Move; });
                                is_Move = true;
                            } while (GetNextGround(GroundType.ICE));

                            if (GetNextGround())
                            {
                                transform.DOMove(transform.position + dir, moveDur).OnComplete(() => { is_Move = false; });
                            }
                            else
                            {
                                is_Move = false;
                            }

                            break;
                    }
                }
            }

            yield return null;
        }
    }

    bool GetNextGround()
    {
        groundColl = Physics2D.OverlapCircle(transform.position + dir, 0.01f, groundMask);

        if (groundColl != null)
        {
            nextGround = groundColl.GetComponent<GroundScript>();
            return true;
        }

        return false;
    }

    bool GetNextGround(GroundType groundType)
    {
        groundColl = Physics2D.OverlapCircle(transform.position + dir, 0.01f, groundMask);

        if (groundColl != null)
        {
            nextGround = groundColl.GetComponent<GroundScript>();
            if (nextGround.groundType.Equals(groundType)) { return true; }
        }

        return false;
    }

    private void OnDisable()
    {

    }
}
