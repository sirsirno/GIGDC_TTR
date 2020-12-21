using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameManager obj = FindObjectOfType<GameManager>();

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

    public Vector3 MapCenter { get; set; }  // 카메라가 멈출 맵의 중심
    public Vector3 MapInfo { get; set; }  // 맵에 대한 정보 x,y
    public Vector3 MapOrigin { get; set; }  // 맵의 x=0, y=0이 되는 지점
    [HideInInspector]
    public int AllEnemyCount { get; set; }

    [Header("저장될 게임의 정보")]
    public GameInfo playerInfo;
}

[System.Serializable]
public class GameInfo
{
    public float maxHp;
    public float currentHp;
    public float playerDamage;
}