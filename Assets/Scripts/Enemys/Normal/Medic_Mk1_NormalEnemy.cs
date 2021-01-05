using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medic_Mk1_NormalEnemy : NormalEnemy
{
    [Header("레이저의 크기")]
    [SerializeField] private Vector2 lazerSize = Vector2.zero;

    [Header("공격의 방향")]
    [SerializeField] private AttackDir attackDir = AttackDir.DOWN;

    [Header("레이저의 위치")]
    [SerializeField] private Transform attackPos;

    [Header("레이저 경고 시간")]
    [SerializeField] private float warningDur;

    [Header("레이저 공격력")]
    [SerializeField] private float damage;


    private Vector3 lazerRotation = new Vector3(0f, 0f, -180f);
    private Vector3 warnDir;



    #region Awake Add Behaviour
    protected override void Awake()
    {
        base.Awake();

        SetAttackDir(attackDir);

        // 생성될 때
        IState spawned = gameObject.AddComponent<StateSpawned>();

        // 이동 추가
        StateRandomXMove stateRandomMove = gameObject.AddComponent<StateRandomXMove>();
        stateRandomMove.movSpeed = stateDur;
        IState move = stateRandomMove;

        // 레이저 어택 추가
        StateHeal stateHeal = gameObject.AddComponent<StateHeal>();
        IState attack = stateHeal;

        // 죽을 때
        IState die = gameObject.AddComponent<StateDie>();

        // 딕셔너리에 추가
        dicState.Add(NormalEnemyState.SPAWNED, spawned);
        dicState.Add(NormalEnemyState.MOVE, move);
        dicState.Add(NormalEnemyState.ATTACK, attack);
        dicState.Add(NormalEnemyState.DIE, die);

        stateMachine = gameObject.AddComponent<StateMachine>();
    }
    #endregion

    public override void SetAttackDir(AttackDir direction)
    {
        switch (direction)
        {
            case AttackDir.DOWN:

                lazerRotation = new Vector3(0f, 0f, -180f);
                break;

            case AttackDir.UP:

                lazerRotation = new Vector3(0f, 0f, 0f);
                break;

            case AttackDir.LEFT:

                lazerRotation = new Vector3(0f, 0f, 90f);
                break;

            case AttackDir.RIGHT:

                lazerRotation = new Vector3(0f, 0f, -90f);
                break;
        }
    }

    protected override void InsertQueue()
    {
        PoolManager.Instance.InsertQueue(this.gameObject, PoolManager.EnemyType.OBSERVERXMK1);
    }
}
