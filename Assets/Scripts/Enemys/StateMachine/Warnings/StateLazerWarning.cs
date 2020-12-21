using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateLazerWarning : MonoBehaviour, IState
{
    public float maxDis;
    public float warningDur;
    public Vector3 warnDir;
    public Transform warnPos;

    public void OperateEnter()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(warnPos.position, warnDir, maxDis, 1<<8);

        foreach (RaycastHit2D hit in hits)
        {
            GroundScript ground = hit.transform.GetComponent<GroundScript>();

            if (ground != null)
            {
                ground.Warning(warningDur);
            }
        }
    }

    public void OperateUpdate()
    {

    }

    public void OperateExit()
    {

    }
}
