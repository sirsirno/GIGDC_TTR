using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public IState CurrentState { get; private set; }

    public void SetDefaultState(IState state)
    {
        CurrentState = state;
        CurrentState.OperateEnter();
    }

    public void SetState(IState state)
    {
        CurrentState.OperateExit();

        CurrentState = state;

        CurrentState.OperateEnter();
    }

    public void DoOperateUpdate()
    {
        CurrentState.OperateUpdate();
    }
}
