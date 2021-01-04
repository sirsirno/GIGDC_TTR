using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHeal : MonoBehaviour, IState
{
    public float heal { get; set; }

    public void OperateEnter()
    {
        EventManager.Instance.HealAllEnemy(heal);
    }

    public void OperateUpdate()
    {

    }

    public void OperateExit()
    {

    }
}
