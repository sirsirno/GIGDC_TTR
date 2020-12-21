using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StateDie : MonoBehaviour, IState
{
    public void OperateEnter()
    {
        GetComponent<SpriteRenderer>().DOFade(0, 1f);
        transform.DOMove(transform.position + Vector3.up * 5f, 1f);
    }

    public void OperateUpdate()
    {

    }

    public void OperateExit()
    {

    }
}
