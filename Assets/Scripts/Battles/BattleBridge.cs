using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BattleBridge : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.Instance.OnBattleStart += StartBattle;
        EventManager.Instance.OnBattleEnd += EndBattle;
    }

    void StartBattle()
    {
        this.GetComponent<Collider2D>().enabled = false;
        this.GetComponent<SpriteRenderer>().DOColor(Color.black, 1f);
    }

    void EndBattle()
    {
        this.GetComponent<Collider2D>().enabled = true;
        this.GetComponent<SpriteRenderer>().DOColor(Color.white, 1f);
    }

    private void OnDestroy()
    {
        
    }
}
