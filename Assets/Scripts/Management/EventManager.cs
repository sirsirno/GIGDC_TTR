using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    private static EventManager instance = null;
    public static EventManager Instance
    {
        get
        {
            if (instance == null)
            {
                EventManager obj = FindObjectOfType<EventManager>();

                if (obj != null)
                {
                    return obj;
                }

                print("싱글톤 옵젝이 없습니다");
                return null;
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public event Action OnBattleStart;
    public event Action OnBattleEnd;

    public event Action OnInsertBullet;

    public void BattleStart()
    {
        if (OnBattleStart != null)
        {
            OnBattleStart.Invoke();
        }
    }

    public void BattleEnd()
    {
        if (OnBattleEnd != null)
        {
            OnBattleEnd.Invoke();
        }
    }

    public void ClearAllBullet()
    {
        if (OnInsertBullet != null)
        {
            OnInsertBullet.Invoke();
        }
    }
}
