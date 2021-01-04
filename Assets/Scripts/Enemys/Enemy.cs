using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum AttackDir
{
    DOWN,
    UP,
    LEFT,
    RIGHT
}

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("최대 HP")]
    [SerializeField]
    protected float MaxHp;
    protected float currHp;
    protected bool is_Dead;

    protected SpriteRenderer sprRend;

    private readonly Color damagedColor = new Color(1f, 1f, 1f, 0.5f);

    [SerializeField]
    private LayerMask playerAttack;

    private void IncreaseHp(float heal)
    {
        if ((currHp + heal) <= MaxHp)
        {
            currHp += heal;
        }
        else
        {
            currHp = MaxHp;
        }

        print("적이 회복되었습니다");
    }

    protected virtual void Awake()
    {
        sprRend = GetComponent<SpriteRenderer>();
    }

    protected virtual void OnEnable()
    {
        currHp = MaxHp;
        is_Dead = false;
        GameManager.Instance.AllEnemyCount += 1;
        EventManager.Instance.OnHealEnemy += IncreaseHp;
    }

    protected virtual void InsertQueue()
    {
        // 인서트 큐 하세요
    }

    public virtual void SetAttackDir(AttackDir direction)
    {
        // 공격방향 변경
    }

    public virtual void OnDamaged(float damage)
    {
        sprRend.color = damagedColor;
        sprRend.DOColor(Color.white, 0.1f).SetDelay(0.03f);

        currHp -= damage;
        print("적의 현재 체력 : " + currHp);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (is_Dead) { return; }
        Shootable bullet = other.gameObject.GetComponent<Shootable>();
        OnDamaged(bullet.Damage);
        if (bullet.gameObject.activeSelf.Equals(true))
            bullet.InsertQueue();
    }

    protected virtual void OnDisable()
    {
        currHp = MaxHp;
        is_Dead = false;
        if(GameManager.Instance != null)
            GameManager.Instance.AllEnemyCount -= 1;
        if (EventManager.Instance != null)
            EventManager.Instance.OnHealEnemy -= IncreaseHp;
    }
}
