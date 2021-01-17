using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LazerTurret : Turret
{
    public float AttackDur { get; set; }

    private Vector3 warnDir;
    private Vector3 lazerRotation;

    [Header("레이저의 위치")]
    [SerializeField]
    private Transform attackPos;

    [Header("레이저의 크기")]
    public Vector2 lazerSize;

    [Header("레이저 경고 시간")]
    [SerializeField]
    public float warningDur;

    [Header("공격경고 후 공격까지의 속도")]
    [SerializeField]
    private float attackDur = 1f;

    [Header("전체적인 속도(낮을수록 빠름)")]
    [SerializeField]
    public float moveDur = 1f;

    [Header("레이저 인생 시간")]
    public float lazerLifetime;

    public WaitForSeconds attackWait;
    private Coroutine lifetime;

    private StateSpawned stateSpawned;
    private StateLazerWarning stateLazerWarning;
    private StateRedLazerAttack stateLazerAttack;
    private StateDie stateDie;

    private void Awake()
    {
        stateSpawned = gameObject.AddComponent<StateSpawned>();

        stateLazerWarning = gameObject.AddComponent<StateLazerWarning>();
    
        stateLazerWarning.maxDis = lazerSize.y;
        stateLazerWarning.warnPos = attackPos;
        stateLazerWarning.warningDur = warningDur;

        stateLazerAttack = gameObject.AddComponent<StateRedLazerAttack>();

        stateLazerAttack.attackPos = attackPos;
        stateLazerAttack.lazerSize = lazerSize;

        stateDie = gameObject.AddComponent<StateDie>();
    }

    private void OnEnable()
    {
        attackWait = new WaitForSeconds(attackDur);
    }

    public override void OnSpawned()
    {
        stateSpawned.OperateEnter();
    }

    public override void WarningAndShoot()
    {
        stateLazerWarning.warnDir = warnDir;
        lifetime = StartCoroutine(ShootLazer());
    }

    public override void Shoot()
    {
        stateLazerAttack.lazerRotation = lazerRotation;
        stateLazerAttack.lazerSize = lazerSize;

        stateLazerAttack.OperateEnter();
        stateLazerAttack.lazer.LazerLifetime = lazerLifetime;
        stateLazerAttack.lazer.transform.parent = transform;
    }

    private IEnumerator ShootLazer()
    {
        stateLazerWarning.OperateEnter();

        yield return attackWait;

        stateLazerAttack.lazerRotation = lazerRotation;
        stateLazerAttack.lazerSize = lazerSize;

        stateLazerAttack.OperateEnter();
        stateLazerAttack.lazer.LazerLifetime = lazerLifetime;
        stateLazerAttack.lazer.transform.parent = transform;
    }

    public override void OnDie()
    {
        StartCoroutine(Die());
        StopCoroutine(lifetime);
    }

    public override void StopLifetime()
    {
        StopCoroutine(lifetime);
        stateDie.OperateEnter();
    }

    private IEnumerator Die()
    {
        stateDie.OperateEnter();
        yield return new WaitForSeconds(1.2f);
        InsertQueue();
    }

    protected override void InsertQueue()
    {
        PoolManager.Instance.InsertQueue(this.gameObject, PoolManager.EnemyTurret.LAZER);
    }

    public override void SetAttackDir(AttackDir direction)
    {
        switch (direction)
        {
            case AttackDir.DOWN:

                warnDir = Vector3.down;
                lazerRotation = new Vector3(0f, 0f, -180f);
                transform.DORotate(new Vector3(0f, 0f, 0f), 0.3f);
                currentDir = direction;
                move_X = true;
                break;

            case AttackDir.UP:

                warnDir = Vector3.up;
                lazerRotation = new Vector3(0f, 0f, 0f);
                transform.DORotate(new Vector3(0f, 0f, -180f), 0.3f);
                currentDir = direction;
                move_X = true;
                break;

            case AttackDir.LEFT:

                warnDir = Vector3.left;
                lazerRotation = new Vector3(0f, 0f, 90f);
                transform.DORotate(new Vector3(0f, 0f, -90f), 0.3f);
                currentDir = direction;
                move_X = false;
                break;

            case AttackDir.RIGHT:

                warnDir = Vector3.right;
                lazerRotation = new Vector3(0f, 0f, -90f);
                transform.DORotate(new Vector3(0f, 0f, 90f), 0.3f);
                currentDir = direction;
                move_X = false;
                break;
        }
    }
}
