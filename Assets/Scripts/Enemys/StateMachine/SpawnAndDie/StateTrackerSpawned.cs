using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StateTrackerSpawned : MonoBehaviour, IState
{
    public float MoveDur { get; set; }

    [HideInInspector]
    public GameObject turretPf { get; set; }
    private Turret turret;

    public void OperateEnter()
    {
        turret = Instantiate(turretPf, new Vector3(GameManager.Instance.MapOrigin.x + GameManager.Instance.MapInfo.x + 2f, GameManager.Instance.MapOrigin.y + 2f), Quaternion.identity)
                .GetComponent<Turret>();
        turret.SetAttackDir(AttackDir.LEFT);
        turret.OnSpawned();
        GetComponentInParent<Tracker_Mk1_NormalEnemy>().turret = turret;

        GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<SpriteRenderer>().DOFade(0, 1f).From();
        transform.DOMove(transform.position + Vector3.up * 5f, 1f).From();
    }

    public void OperateUpdate()
    {

    }

    public void OperateExit()
    {

    }
}
