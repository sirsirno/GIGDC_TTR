using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowLazer : Lazer
{
    public override void InsertQueue()
    {
        PoolManager.Instance.InsertQueue(this.gameObject, PoolManager.LazerType.YELLOW);
    }
}
