using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdamBoss : BossEnemy
{
    private enum AdamSkill
    {
        PATTERNONE,
        PATTERNTWO,
        PATTERNTHREE
    }
    
    [Header("레이저의 크기")]
    [SerializeField]
    private Vector2 lazerSize = new Vector2(0.45f, 8f);

    [Header("공격의 방향")]
    [SerializeField]
    private AttackDir attackDir = AttackDir.DOWN;

    private Vector3 lazerRotation = new Vector3(0f, 0f, -180f);
    private Vector3 warnDir;

    [Header("레이저의 위치")]
    [SerializeField]
    private Transform attackPos;

    [Header("레이저 경고 시간")]
    [SerializeField]
    private float warningDur;

    [Header("레이저 터렛들 위치정보")]
    [SerializeField]
    private List<TurretInfo> turretInfos;

    [Header("페이스2 터렛 이동속도")]
    [SerializeField]
    private float phaseTwo_MovSpeed = 3f;

    private WaitForSeconds phaseTwo_Wait;

    private List<Turret> turrets = new List<Turret>();
    private Turret turret;
    private Vector3 temp = Vector3.zero;

    private Dictionary<AdamSkill, BossSkill> dicSkill = new Dictionary<AdamSkill, BossSkill>();
    private SkillAdamPatternOne patternOne;
    private SkillAdamPatternTwo patternTwo;

    private IState move;
    private StateRandomXMove stateRandomMove;
    private StateMoveCenter stateMoveCenter;

    private int attackCount = 6;

    protected override void Awake()
    {
        base.Awake();

        SetAttackDir(attackDir);

        // 생성될 때
        IState spawned = gameObject.AddComponent<StateSpawned>();

        // 랜덤한 X로 이동 추가
        stateRandomMove = gameObject.AddComponent<StateRandomXMove>();
        stateRandomMove.movSpeed = stateDur;
        move = stateRandomMove;
        // 중앙으로 오는 이동 추가
        stateMoveCenter = gameObject.AddComponent<StateMoveCenter>();
        stateMoveCenter.MovSpeed = 0.7f;
        IState moveCenter = stateMoveCenter;

        // 페이스1 추가
        patternOne = gameObject.AddComponent<SkillAdamPatternOne>();
        patternOne.MovSpeed = 0.7f;
        dicSkill.Add(AdamSkill.PATTERNONE, patternOne);

        // 페이스2 추가
        patternTwo = gameObject.AddComponent<SkillAdamPatternTwo>();
        patternTwo.MovSpeed = phaseTwo_MovSpeed;
        dicSkill.Add(AdamSkill.PATTERNTWO, patternTwo);

        // 레이저 경고 추가
        StateLazerWarning stateLazerWarning = gameObject.AddComponent<StateLazerWarning>();
        stateLazerWarning.maxDis = lazerSize.y;
        stateLazerWarning.warningDur = warningDur;
        stateLazerWarning.warnDir = warnDir;
        stateLazerWarning.warnPos = attackPos;
        IState warning = stateLazerWarning;

        // 레이저 어택 추가
        StateLazerAttack stateLazerAttack = gameObject.AddComponent<StateLazerAttack>();
        stateLazerAttack.lazerSize = lazerSize;
        stateLazerAttack.lazerRotation = lazerRotation;
        stateLazerAttack.attackPos = attackPos;
        IState attack = stateLazerAttack;

        // 죽을 때
        IState die = gameObject.AddComponent<StateDie>();

        // 딕셔너리에 추가
        dicState.Add(BossEnemyState.SPAWNED, spawned);
        dicState.Add(BossEnemyState.MOVE, move);
        dicState.Add(BossEnemyState.WARNING, warning);
        dicState.Add(BossEnemyState.ATTACK, attack);
        dicState.Add(BossEnemyState.DIE, die);

        stateMachine = gameObject.AddComponent<StateMachine>();

        phaseTwo_Wait = new WaitForSeconds(8f - phaseTwo_MovSpeed + 1f);
    }

    protected override IEnumerator PhaseOne()
    {
        yield return null;

        CreateTurret();
        stateMachine.SetDefaultState(dicState[BossEnemyState.SPAWNED]);
        yield return new WaitForSeconds(1.5f);

        while (true)
        {
            if (stateMachine.CurrentState.Equals(dicState[BossEnemyState.SPAWNED]))
            {
                yield return stateWait;
                CheckPhase();
                stateMachine.SetState(dicState[BossEnemyState.MOVE]);
                StartCoroutine(dicSkill[AdamSkill.PATTERNONE].UseSkill());
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
                StartCoroutine(dicSkill[AdamSkill.PATTERNONE].UseSkill());
            }

            yield return null;
        }
    }

    private IEnumerator PhaseTwo()
    {
        yield return null;
        move = stateMoveCenter;
        dicState[BossEnemyState.MOVE] = move;
        stateMachine.SetState(dicState[BossEnemyState.MOVE]);
        yield return new WaitForSeconds(1.5f);

        while (true)
        {
            if (stateMachine.CurrentState.Equals(dicState[BossEnemyState.MOVE]))
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
                if (attackCount < 5)
                {
                    attackCount++;
                    move = stateRandomMove;
                    dicState[BossEnemyState.MOVE] = move;
                    stateMachine.SetState(dicState[BossEnemyState.MOVE]);
                    StartCoroutine(dicSkill[AdamSkill.PATTERNONE].UseSkill());
                }
                else
                {
                    attackCount = 0;
                    move = stateMoveCenter;
                    dicState[BossEnemyState.MOVE] = move;
                    stateMachine.SetState(dicState[BossEnemyState.MOVE]);
                    StartCoroutine(dicSkill[AdamSkill.PATTERNTWO].UseSkill());
                    yield return phaseTwo_Wait;
                }
            }

            yield return null;
        }
    }

    private void CreateTurret()
    {
        foreach (TurretInfo turretInfo in turretInfos)
        {
            turret = PoolManager.Instance.GetQueue(PoolManager.EnemyTurret.LAZER).GetComponent<Turret>();
            temp.x = turretInfo.pos.x - 1;
            temp.y = turretInfo.pos.y - 1;
            turret.transform.position = GameManager.Instance.MapOrigin + temp;
            turret.SetAttackDir(turretInfo.attackDir);
            turrets.Add(turret);
        }

        patternOne.turrets = turrets;
        patternTwo.turrets = turrets;

        foreach (Turret obj in turrets)
        {
            obj.OnSpawned();
        }
    }

    protected override void CheckPhase()
    {
        if (currHp <= (MaxHp * 50f / 100f))     // 50퍼 이하
        {
            if (currHp <= (MaxHp * 30f / 100f))     // 30퍼 이하
            {
                if (currentPhase.Equals(2))
                {
                    currentPhase++;
                    print("페이즈 3 발동");

                    stateWait = new WaitForSeconds(0.65f);  // 자동으로 돌아감
                    attackWait = new WaitForSeconds(0.7f);  // 자동으로 돌아감
                    stateRandomMove.movSpeed = 0.65f;
                    patternOne.MovSpeed = 0.65f;    // 터렛 움직임
                    patternTwo.MovSpeed = 6f;       // 터렛 움직임
                    foreach (LazerTurret turret in turrets)
                    {
                        turret.attackWait = new WaitForSeconds(0.7f);  // 자동으로 돌아감
                    }
                    StopCoroutine(lifetime);
                    lifetime = StartCoroutine(PhaseTwo());
                }

                return;
            }

            if (currentPhase.Equals(1))
            {
                currentPhase++;
                print("페이즈 2 발동");
                StopCoroutine(lifetime);
                lifetime = StartCoroutine(PhaseTwo());
            }
        }
    }

    public override void SetAttackDir(AttackDir direction)
    {
        switch (direction)
        {
            case AttackDir.DOWN:

                warnDir = Vector3.down;
                lazerRotation = new Vector3(0f, 0f, -180f);
                break;

            case AttackDir.UP:

                warnDir = Vector3.up;
                lazerRotation = new Vector3(0f, 0f, 0f);
                break;

            case AttackDir.LEFT:

                warnDir = Vector3.left;
                lazerRotation = new Vector3(0f, 0f, 90f);
                break;

            case AttackDir.RIGHT:

                warnDir = Vector3.right;
                lazerRotation = new Vector3(0f, 0f, -90f);
                break;
        }
    }

    protected override void InsertQueue()
    {
        foreach (Turret obj in turrets)
        {
            obj.OnDie();
        }
        turrets.Clear();
        PoolManager.Instance.InsertQueue(this.gameObject, PoolManager.EnemyType.BOSSADAM);
    }

    protected override IEnumerator OnDie()
    {
        StartCoroutine(base.OnDie());
        attackCount = 5;
        stateRandomMove.movSpeed = 0.7f;
        patternOne.MovSpeed = 0.7f;
        patternTwo.MovSpeed = 5f;
        yield return null;
    }
}

[System.Serializable]
public struct TurretInfo
{
    public Vector3 pos;
    public AttackDir attackDir;
}
