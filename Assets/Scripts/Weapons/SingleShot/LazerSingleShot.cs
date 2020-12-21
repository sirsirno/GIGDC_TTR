using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LazerSingleShot: SingleShot
{
    /*
      Weapon 기본적인 파라미터들 
        protected enum WeaponType { Primary, Secondary };

        protected float useCooltime;
        protected WeaponType weaponType;
        protected string weaponId;

        protected Transform shootPos;

        protected bool is_Cooldown;
     */

    private GameObject bullet;

    protected override void GetBullet()
    {
        Bullet bullet = PoolManager.Instance.GetQueue(PoolManager.BulletType.LAZER).GetComponent<Bullet>();
        bullet.transform.position = shootPos.position;
        bullet.transform.rotation = shootPos.rotation;
        bullet.Damage = gunDamage;
    }
}
