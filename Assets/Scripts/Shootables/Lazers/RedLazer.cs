using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLazer : Lazer
{

    public override void InsertQueue()
    {
        PoolManager.Instance.InsertQueue(this.gameObject, PoolManager.LazerType.RED);
    }
}
