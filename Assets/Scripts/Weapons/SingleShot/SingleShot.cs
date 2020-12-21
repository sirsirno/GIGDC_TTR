using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShot : Weapon
{
    [Header("무기 설정")]
    [SerializeField]
    protected float useCooltime;
    [SerializeField]
    protected int shotRepeat;
    public int ShotRepeat
    {
        get
        {
            return shotRepeat;
        }
        set
        {
            if (shotRepeat >= 3)
            {
                return;
            }

            shotRepeat = value;
        }
    }

    [SerializeField]
    protected float gunDamage;
    [SerializeField]
    protected bool is_AutoShot = false;

    [Space(10f)]
    [Header("사용할 위치 설정")]
    [SerializeField]
    protected Transform shootPos;

    protected bool is_Cooldown;

    protected override void UseWeapon()
    {
        GetBullet();
    }

    protected virtual void GetBullet()
    {
        //가져올 풀링 옵젝 GetQueue하세요
    }

    private void OnEnable()
    {
        StartCoroutine(UseCoolDown());
    }

    private IEnumerator UseCoolDown()
    {
        WaitForSeconds cooltime = new WaitForSeconds(useCooltime);
        WaitForSeconds shootDelay = new WaitForSeconds(0.1f);

        yield return null;

        while (true)
        {
            if (!is_Cooldown && (Input.GetMouseButton((int)weaponType) || is_AutoShot))
            {
                is_Cooldown = true;

                if (shotRepeat > 1)
                {
                    for (int i = 0; i < shotRepeat; i++)
                    {
                        UseWeapon();
                        yield return shootDelay;
                    }
                }
                else
                {
                    UseWeapon();
                }

                yield return cooltime;
                is_Cooldown = false;
            }

            yield return null;
        }
    }
}
