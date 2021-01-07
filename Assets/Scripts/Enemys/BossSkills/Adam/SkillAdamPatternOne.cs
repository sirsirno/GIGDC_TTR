using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkillAdamPatternOne : BossSkill
{
    // 터렛들
    public List<Turret> turrets = new List<Turret>();

    private Vector3 moveDir = Vector3.zero;
    private int randX = 0;
    private int randY = 0;
    public float MovSpeed { private get; set; }

    private WaitForSeconds moveWait;

    private void Start()
    {
        moveWait = new WaitForSeconds(MovSpeed);
    }

    public override IEnumerator UseSkill()
    {
        foreach (Turret turret in turrets)
        {
            if (turret.move_X)
            {
                do
                {
                    randX = (int)Random.Range(GameManager.Instance.MapOrigin.x - 1, GameManager.Instance.MapOrigin.x + GameManager.Instance.MapInfo.x);
                } while (turret.transform.position.x.Equals(randX));
                moveDir.x = randX;
                moveDir.y = turret.transform.position.y;

                turret.transform.DOMove(moveDir, MovSpeed);
            }
            else
            {
                do
                {
                    randY = (int)Random.Range(GameManager.Instance.MapOrigin.y, GameManager.Instance.MapOrigin.y + GameManager.Instance.MapInfo.y);
                } while (turret.transform.position.y.Equals(randY));
                moveDir.x = turret.transform.position.x;
                moveDir.y = randY;

                turret.transform.DOMove(moveDir, MovSpeed);
            }
        }

        yield return moveWait;

        foreach (Turret turret in turrets)
        {
            turret.WarningAndShoot();
        }
    }
}
