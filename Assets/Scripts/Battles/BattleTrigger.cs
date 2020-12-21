using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTrigger : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.Instance.OnBattleStart += DisableTrigger;
    }

    void StartBattle()
    {
        EventManager.Instance.BattleStart();
    }

    void DisableTrigger()
    {
        EventManager.Instance.OnBattleStart -= DisableTrigger;
        Destroy(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartBattle();
    }
}
