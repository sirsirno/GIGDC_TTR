using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraScript : MonoBehaviour
{
    [Header("타겟설정")]
    [SerializeField]
    private GameObject target;
    [Header("따라갈 시간")]
    [SerializeField]
    private float followDur = 1f;
    [Header("오프셋")]
    [SerializeField]
    private Vector2 offset = Vector2.zero;

    private Vector3 targetDir;
    private bool is_Battle;
    private Camera mainCam;

    private void Start()
    {
        mainCam = GetComponent<Camera>();
        EventManager.Instance.OnBattleStart += BattleStart;
        EventManager.Instance.OnBattleEnd += BattleEnd;
    }

    private void FixedUpdate()
    {
        if (!is_Battle)
            FollowTarget();
        else
            BattleFollow();
    }

    void FollowTarget()
    {
        //mainCam.DOOrthoSize(4.5f, 0.5f);
        targetDir.Set(target.transform.position.x + offset.x, target.transform.position.y + offset.y, this.transform.position.z);
        transform.DOMove(targetDir, followDur);
    }

    void BattleFollow()
    {
        //mainCam.DOOrthoSize(6f, 0.5f);
        targetDir.Set(GameManager.Instance.MapCenter.x + offset.x, GameManager.Instance.MapCenter.y + offset.y, this.transform.position.z);
        transform.DOMove(targetDir, followDur);
    }

    void BattleStart()
    {
        is_Battle = true;
    }

    void BattleEnd()
    {
        is_Battle = false;
    }

    private void OnDisable()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.OnBattleStart -= BattleStart;
            EventManager.Instance.OnBattleEnd -= BattleEnd;
        }
    }
    //잘놀다 갑니다.
}
