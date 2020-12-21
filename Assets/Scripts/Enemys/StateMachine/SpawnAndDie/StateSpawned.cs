using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StateSpawned : MonoBehaviour, IState
{
    public float MoveDur { get; set; }

    public void OperateEnter()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<SpriteRenderer>().DOFade(0, 1f).From();
        transform.DOMove(transform.position + Vector3.up * 5f, 1f).From();
    }

    public void OperateUpdate()
    {

    }

    public void OperateExit()
    {

    }
}
