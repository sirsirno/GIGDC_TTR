using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkillAdamPatternTwo : BossSkill
{
    // 터렛들
    public List<Turret> turrets = new List<Turret>();

    public float MovSpeed { private get; set; }

    private float random_UpOrDown = 0f;
    private float temp_lazerLifetime = 0f;
    private bool changeUpDown = false;
    private Vector2 temp_lazerSize = Vector2.zero;

    private List<Vector2> temp_turretPos = new List<Vector2>();
    private List<AttackDir> temp_turretDir = new List<AttackDir>();

    private WaitForSeconds moveWait;
    private Vector3 movePos = Vector3.zero;

    private void Start()
    {
        moveWait = new WaitForSeconds(8f - MovSpeed + 1f);
    }

    public override IEnumerator UseSkill()
    {
        foreach (LazerTurret turret in turrets)
        {
            temp_turretPos.Add(turret.transform.position);
            temp_turretDir.Add(turret.currentDir);

            temp_lazerLifetime = turret.lazerLifetime;
            turret.lazerLifetime = 9f - MovSpeed;

            /*
            random_UpOrDown = Random.value;
            if (random_UpOrDown <= 0.5f)
            {
                turret.SetAttackDir(AttackDir.DOWN);
                turret.transform.DOMoveY(GameManager.Instance.MapOrigin.y + GameManager.Instance.MapInfo.y + 2f, 0.5f);
            }
            else
            {
                turret.SetAttackDir(AttackDir.UP);
                turret.transform.DOMoveY(GameManager.Instance.MapOrigin.y - 3f, 0.5f);
            }
            */

            if (changeUpDown)
            {
                changeUpDown = false;
                turret.SetAttackDir(AttackDir.DOWN);
                turret.transform.DOMoveY(GameManager.Instance.MapOrigin.y + GameManager.Instance.MapInfo.y + 2f, 0.5f);
            }
            else
            {
                changeUpDown = true;
                turret.SetAttackDir(AttackDir.UP);
                turret.transform.DOMoveY(GameManager.Instance.MapOrigin.y - 3f, 0.5f);
            }
        }

        turrets[0].transform.DOMoveX(GameManager.Instance.MapOrigin.x - 7f, 0.5f);
        turrets[1].transform.DOMoveX(GameManager.Instance.MapOrigin.x - 3f, 0.5f);
        turrets[2].transform.DOMoveX(GameManager.Instance.MapOrigin.x + GameManager.Instance.MapInfo.x + 2f, 0.5f);
        turrets[3].transform.DOMoveX(GameManager.Instance.MapOrigin.x + GameManager.Instance.MapInfo.x + 6f, 0.5f);

        foreach (LazerTurret turret in turrets)
        {
            temp_lazerSize = turret.lazerSize;
            turret.lazerSize.y = 5f;
            turret.Shoot();
        }

        yield return new WaitForSeconds(1f);

        turrets[0].transform.DOMoveX(GameManager.Instance.MapOrigin.x + GameManager.Instance.MapInfo.x + 6f, MovSpeed).SetEase(Ease.Linear).SetSpeedBased();
        turrets[1].transform.DOMoveX(GameManager.Instance.MapOrigin.x + GameManager.Instance.MapInfo.x + 2f, MovSpeed).SetEase(Ease.Linear).SetSpeedBased();
        turrets[2].transform.DOMoveX(GameManager.Instance.MapOrigin.x - 3f, MovSpeed).SetEase(Ease.Linear).SetSpeedBased();
        turrets[3].transform.DOMoveX(GameManager.Instance.MapOrigin.x - 7f, MovSpeed).SetEase(Ease.Linear).SetSpeedBased();
        
        yield return moveWait;

        int i = 0;
        foreach (LazerTurret turret in turrets)
        {
            turret.SetAttackDir(temp_turretDir[i]);
            turret.transform.DOMove(temp_turretPos[i], 0.5f);
            turret.lazerLifetime = temp_lazerLifetime;
            turret.lazerSize = temp_lazerSize;
            i++;
        }

        temp_turretDir.Clear();
        temp_turretPos.Clear();

        yield return null;
    }
}
