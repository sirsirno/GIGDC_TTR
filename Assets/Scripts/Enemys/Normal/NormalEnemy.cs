using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NormalEnemy : Enemy
{
    public enum NormalEnemyState
    {
        SPAWNED,
        MOVE,
        WARNING,
        ATTACK,
        DIE
    }

    protected Dictionary<NormalEnemyState, IState> dicState = new Dictionary<NormalEnemyState, IState>();

    protected StateMachine stateMachine;

    [Header("전체적인 속도 (낮을수록 빨라짐)")]
    [SerializeField]
    protected float stateDur = 1f;
    protected WaitForSeconds stateWait;

    [Header("공격경고 후 공격까지의 속도")]
    [SerializeField]
    protected float attackDur = 1f;
    protected WaitForSeconds attackWait;

    protected WaitForSeconds dieWait = new WaitForSeconds(1.2f);
    private Coroutine lifetime;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        stateWait = new WaitForSeconds(stateDur);
        attackWait = new WaitForSeconds(attackDur);
        lifetime = StartCoroutine(StateManagement());
    }

    protected virtual IEnumerator StateManagement()
    {
        yield return null;
        stateMachine.SetDefaultState(dicState[NormalEnemyState.SPAWNED]);

        while (true)
        {
            if (stateMachine.CurrentState.Equals(dicState[NormalEnemyState.SPAWNED]))
            {
                yield return stateWait;
                stateMachine.SetState(dicState[NormalEnemyState.MOVE]);
            }
            else if (stateMachine.CurrentState.Equals(dicState[NormalEnemyState.MOVE]))
            {
                yield return stateWait;
                stateMachine.SetState(dicState[NormalEnemyState.WARNING]);
            }
            else if (stateMachine.CurrentState.Equals(dicState[NormalEnemyState.WARNING]))
            {
                yield return attackWait;
                stateMachine.SetState(dicState[NormalEnemyState.ATTACK]);
            }
            else if (stateMachine.CurrentState.Equals(dicState[NormalEnemyState.ATTACK]))
            {
                yield return stateWait;
                stateMachine.SetState(dicState[NormalEnemyState.MOVE]);
            }

            yield return null;
        }
    }

    protected virtual IEnumerator OnDie()
    {
        yield return dieWait;
        InsertQueue();
    }

    protected void CheckHp()
    {
        if (currHp <= 0f)
        {
            stateMachine.SetState(dicState[NormalEnemyState.DIE]);
            StopCoroutine(lifetime);
            lifetime = StartCoroutine(OnDie());
            is_Dead = true;
        }
    }

    public override void SetAttackDir(AttackDir direction)
    {
        // 공격방향 변경
    }

    public override void OnDamaged(float damage)
    {
        base.OnDamaged(damage);
        CheckHp();
    }
}
