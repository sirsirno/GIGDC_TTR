using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    public enum BossEnemyState
    {
        SPAWNED,
        MOVE,
        NEXTPHASE,
        WARNING,
        ATTACK,
        DIE
    }

    protected Dictionary<BossEnemyState, IState> dicState = new Dictionary<BossEnemyState, IState>();

    protected StateMachine stateMachine;
    protected int currentPhase = 1;

    [Header("전체적인 속도 (낮을수록 빨라짐)")]
    [SerializeField]
    protected float stateDur = 1f;
    protected WaitForSeconds stateWait;

    [Header("공격경고 후 공격까지의 속도")]
    [SerializeField]
    protected float attackDur = 1f;
    protected WaitForSeconds attackWait;

    protected WaitForSeconds dieWait = new WaitForSeconds(1.2f);
    protected Coroutine lifetime;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        stateWait = new WaitForSeconds(stateDur);
        attackWait = new WaitForSeconds(attackDur);
        lifetime = StartCoroutine(PhaseOne());
    }

    protected virtual IEnumerator PhaseOne()
    {
        yield return null;
        stateMachine.SetDefaultState(dicState[BossEnemyState.SPAWNED]);

        while (true)
        {
            if (stateMachine.CurrentState.Equals(dicState[BossEnemyState.SPAWNED]))
            {
                yield return stateWait;
                CheckPhase();
                stateMachine.SetState(dicState[BossEnemyState.MOVE]);
            }
            else if (stateMachine.CurrentState.Equals(dicState[BossEnemyState.MOVE]))
            {
                yield return stateWait;
                CheckPhase();
                stateMachine.SetState(dicState[BossEnemyState.WARNING]);
            }
            else if (stateMachine.CurrentState.Equals(dicState[BossEnemyState.WARNING]))
            {
                yield return attackWait;
                CheckPhase();
                stateMachine.SetState(dicState[BossEnemyState.ATTACK]);
            }
            else if (stateMachine.CurrentState.Equals(dicState[BossEnemyState.ATTACK]))
            {
                yield return stateWait;
                CheckPhase();
                stateMachine.SetState(dicState[BossEnemyState.MOVE]);
            }

            yield return null;
        }
    }

    protected virtual IEnumerator OnDie()
    {
        yield return dieWait;
        currentPhase = 1;
        InsertQueue();
    }

    protected virtual void CheckPhase()
    {
        // 페이스 비교하여 dicState 변경
    }

    protected void CheckHp()
    {
        if (currHp <= 0f)
        {
            stateMachine.SetState(dicState[BossEnemyState.DIE]);
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
