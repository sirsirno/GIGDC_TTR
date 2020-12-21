using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public enum WType { Primary, Secondary };

    [SerializeField]
    protected WType weaponType;
    public WType WeaponType
    {
        get
        {
            return weaponType;
        }
    }

    [SerializeField]
    protected string weaponId;
    public string WeaponId
    {
        get
        {
            return weaponId;
        }
    }

    protected abstract void UseWeapon();
}
