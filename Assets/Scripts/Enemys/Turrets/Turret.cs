using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Turret : MonoBehaviour
{
    [SerializeField]
    public bool move_X = false;
    public AttackDir currentDir;

    public abstract void OnSpawned();
    public abstract void Shoot();
    public abstract void OnDie();
    protected abstract void InsertQueue();
    public abstract void SetAttackDir(AttackDir direction);
}
