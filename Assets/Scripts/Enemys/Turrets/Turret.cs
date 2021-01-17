using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Turret : MonoBehaviour
{
    public bool move_X = false;
    public AttackDir currentDir;

    public abstract void OnSpawned();
    public abstract void WarningAndShoot();
    public abstract void Shoot();
    public abstract void OnDie();
    public abstract void StopLifetime();
    protected abstract void InsertQueue();
    public abstract void SetAttackDir(AttackDir direction);
}
