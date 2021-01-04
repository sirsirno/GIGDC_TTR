using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private static PoolManager instance = null;
    public static PoolManager Instance
    {
        get
        {
            if (instance == null)
            {
                PoolManager obj = FindObjectOfType<PoolManager>();

                if(obj != null)
                {
                    return obj;
                }

                print("싱글톤 옵젝이 없습니다");
                return null;
            }
            return instance;
        }
    }

    public enum BulletType
    {
        LAZER,
        BEAM
    }

    public enum LazerType
    {
        RED,
        YELLOW
    }

    public enum EnemyType 
    { 
        OBSERVERXMK1,
        OBSERVERYMK2_LEFT,
        OBSERVERYMK2_RIGHT,
        BOSSADAM
    }

    public enum EnemyTurret
    {
        LAZER
    }

    // 총알 정보

    [Header("레이저 총알 풀 정보")]
    [SerializeField]
    private GameObject bullet_Lazer;
    private Queue<GameObject> bullet_Lazer_Pool = new Queue<GameObject>();
    [SerializeField]
    private int bullet_Lazer_Length;

    [Header("빔 총알 풀 정보")]
    [SerializeField]
    private GameObject bullet_Beam;
    private Queue<GameObject> bullet_Beam_Pool = new Queue<GameObject>();
    [SerializeField]
    private int bullet_Beam_Length;

    // 레이저 정보

    [Header("빨간 레이저 풀 정보")]
    [SerializeField]
    private GameObject lazer_Red;
    private Queue<GameObject> lazer_Red_Pool = new Queue<GameObject>();
    [SerializeField]
    private int lazer_Red_Length;

    [Header("다란 레이저 풀 정보")]
    [SerializeField]
    private GameObject lazer_Yellow;
    private Queue<GameObject> lazer_Yellow_Pool = new Queue<GameObject>();
    [SerializeField]
    private int lazer_Yellow_Length;

    // 적 정보

    [Header("감시자X Mk.1 풀 정보")]
    [SerializeField]
    private GameObject enemy_ObserverX_Mk1;
    private Queue<GameObject> enemy_ObserverX_Mk1_Pool = new Queue<GameObject>();
    [SerializeField]
    private int enemy_ObserverX_Mk1_Length;

    [Header("감시자Y Mk.2(왼쪽) 풀 정보")]
    [SerializeField]
    private GameObject enemy_ObserverY_Mk2_Left;
    private Queue<GameObject> enemy_ObserverY_Mk2_Left_Pool = new Queue<GameObject>();
    [SerializeField]
    private int enemy_ObserverY_Mk2_Left_Length;

    [Header("감시자Y Mk.2(오른쪽) 풀 정보")]
    [SerializeField]
    private GameObject enemy_ObserverY_Mk2_Right;
    private Queue<GameObject> enemy_ObserverY_Mk2_Right_Pool = new Queue<GameObject>();
    [SerializeField]
    private int enemy_ObserverY_Mk2_Right_Length;
    
    // 보스 적 정보

    [Header("적 아담 풀 정보")]
    [SerializeField]
    private GameObject boss_Adam;
    private Queue<GameObject> boss_Adam_Pool = new Queue<GameObject>();
    [SerializeField]
    private int boss_Adam_Length;

    // 레이저 정보

    [Header("포탑 레이저 풀 정보")]
    [SerializeField]
    private GameObject turret_Lazer;
    private Queue<GameObject> turret_Lazer_Pool = new Queue<GameObject>();
    [SerializeField]
    private int turret_Lazer_Length;


    private void Awake()
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

        for (int i = 0; i < bullet_Lazer_Length; i++) // 레이저 총알
        {
            GameObject obj = Instantiate(bullet_Lazer, transform);
            bullet_Lazer_Pool.Enqueue(obj);
            obj.SetActive(false);
        }

        for (int i = 0; i < bullet_Beam_Length; i++) // 빔 총알
        {
            GameObject obj = Instantiate(bullet_Beam, transform);
            bullet_Beam_Pool.Enqueue(obj);
            obj.SetActive(false);
        }

        for (int i = 0; i < enemy_ObserverX_Mk1_Length; i++) // 노말 레이저 적
        {
            GameObject obj = Instantiate(enemy_ObserverX_Mk1, transform);
            enemy_ObserverX_Mk1_Pool.Enqueue(obj);
            obj.SetActive(false);
        }

        for (int i = 0; i < enemy_ObserverY_Mk2_Left_Length; i++) // 노말 사이드 왼쪽 레이저 적
        {
            GameObject obj = Instantiate(enemy_ObserverY_Mk2_Left, transform);
            enemy_ObserverY_Mk2_Left_Pool.Enqueue(obj);
            obj.SetActive(false);
        }

        for (int i = 0; i < enemy_ObserverY_Mk2_Right_Length; i++) // 노말 사이드 오른쪽 레이저 적
        {
            GameObject obj = Instantiate(enemy_ObserverY_Mk2_Right, transform);
            enemy_ObserverY_Mk2_Right_Pool.Enqueue(obj);
            obj.SetActive(false);
        }

        for (int i = 0; i < boss_Adam_Length; i++) // 보스 아담
        {
            GameObject obj = Instantiate(boss_Adam, transform);
            boss_Adam_Pool.Enqueue(obj);
            obj.SetActive(false);
        }

        for (int i = 0; i < lazer_Red_Length; i++) // 레이저 빨강
        {
            GameObject obj = Instantiate(lazer_Red, transform);
            lazer_Red_Pool.Enqueue(obj);
            obj.SetActive(false);
        }

        for (int i = 0; i < lazer_Yellow_Length; i++) // 레이저 다랑
        {
            GameObject obj = Instantiate(lazer_Yellow, transform);
            lazer_Yellow_Pool.Enqueue(obj);
            obj.SetActive(false);
        }

        for (int i = 0; i < turret_Lazer_Length; i++) // 레이저 터렛
        {
            GameObject obj = Instantiate(turret_Lazer, transform);
            turret_Lazer_Pool.Enqueue(obj);
            obj.SetActive(false);
        }
    }

    public void InsertQueue(GameObject obj, BulletType poolType)
    {
        switch (poolType)
        {
            case BulletType.LAZER:
                bullet_Lazer_Pool.Enqueue(obj);
                obj.SetActive(false);
                break;

            case BulletType.BEAM:
                bullet_Beam_Pool.Enqueue(obj);
                obj.SetActive(false);
                break;
        }
    }
    public void InsertQueue(GameObject obj, EnemyType poolType)
    {
        switch (poolType)
        {
            case EnemyType.OBSERVERXMK1:
                enemy_ObserverX_Mk1_Pool.Enqueue(obj);
                obj.SetActive(false);
                break;

            case EnemyType.OBSERVERYMK2_LEFT:
                enemy_ObserverY_Mk2_Left_Pool.Enqueue(obj);
                obj.SetActive(false);
                break;

            case EnemyType.OBSERVERYMK2_RIGHT:
                enemy_ObserverY_Mk2_Right_Pool.Enqueue(obj);
                obj.SetActive(false);
                break;

            case EnemyType.BOSSADAM:
                obj.transform.parent = transform;
                boss_Adam_Pool.Enqueue(obj);
                obj.SetActive(false);
                break;
        }
    }

    public void InsertQueue(GameObject obj, LazerType poolType)
    {
        switch (poolType)
        {
            case LazerType.RED:
                obj.transform.parent = transform;
                lazer_Red_Pool.Enqueue(obj);
                obj.SetActive(false);
                break;

            case LazerType.YELLOW:
                obj.transform.parent = transform;
                lazer_Yellow_Pool.Enqueue(obj);
                obj.SetActive(false);
                break;
        }
    }

    public void InsertQueue(GameObject obj, EnemyTurret poolType)
    {
        switch (poolType)
        {
            case EnemyTurret.LAZER:
                obj.transform.parent = transform;
                turret_Lazer_Pool.Enqueue(obj);
                obj.SetActive(false);
                break;
        }
    }

    public GameObject GetQueue(BulletType poolType)
    {
        GameObject obj = null;
        
        switch (poolType)
        {
            case BulletType.LAZER:
                obj = bullet_Lazer_Pool.Dequeue();
                obj.SetActive(true);
                return obj;

            case BulletType.BEAM:
                obj = bullet_Beam_Pool.Dequeue();
                obj.SetActive(true);
                return obj;
        }

        Debug.Log("잘못된 풀타입 입력");
        return obj;
    }
    public GameObject GetQueue(EnemyType poolType)
    {
        GameObject obj = null;

        switch (poolType)
        {
            case EnemyType.OBSERVERXMK1:
                obj = enemy_ObserverX_Mk1_Pool.Dequeue();
                obj.SetActive(true);
                return obj;

            case EnemyType.OBSERVERYMK2_LEFT:
                obj = enemy_ObserverY_Mk2_Left_Pool.Dequeue();
                obj.SetActive(true);
                return obj;

            case EnemyType.OBSERVERYMK2_RIGHT:
                obj = enemy_ObserverY_Mk2_Right_Pool.Dequeue();
                obj.SetActive(true);
                return obj;

            case EnemyType.BOSSADAM:
                obj = boss_Adam_Pool.Dequeue();
                obj.SetActive(true);
                return obj;
        }

        Debug.Log("잘못된 풀타입 입력");
        return obj;
    }

    public GameObject GetQueue(LazerType poolType)
    {
        GameObject obj = null;

        switch (poolType)
        {
            case LazerType.RED:
                obj = lazer_Red_Pool.Dequeue();
                obj.SetActive(true);
                return obj;

            case LazerType.YELLOW:
                obj = lazer_Yellow_Pool.Dequeue();
                obj.SetActive(true);
                return obj;
        }

        Debug.Log("잘못된 풀타입 입력");
        return obj;
    }

    public GameObject GetQueue(EnemyTurret poolType)
    {
        GameObject obj = null;

        switch (poolType)
        {
            case EnemyTurret.LAZER:
                obj = turret_Lazer_Pool.Dequeue();
                obj.SetActive(true);
                return obj;
        }

        Debug.Log("잘못된 풀타입 입력");
        return obj;
    }
}
