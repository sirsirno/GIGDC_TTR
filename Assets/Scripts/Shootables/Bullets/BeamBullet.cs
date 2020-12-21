using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamBullet : Bullet
{
    public override void InsertQueue()
    {
        PoolManager.Instance.InsertQueue(this.gameObject, PoolManager.BulletType.BEAM);
    }
}
