using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHeal : MonoBehaviour, IState
{
    public float heal { get; set; } = 5.0f;

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
