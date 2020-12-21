using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerBullet : Bullet
{
    public override void InsertQueue()
    {
        PoolManager.Instance.InsertQueue(this.gameObject, PoolManager.BulletType.LAZER);
    }
}
