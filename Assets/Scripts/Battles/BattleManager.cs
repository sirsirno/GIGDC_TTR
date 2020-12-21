using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [Header("맵의 센터")]
    [SerializeField]
    private Transform mapCenter;
    [Header("맵의 로컬좌표 0,0이 되는 위치")]
    [SerializeField]
    private Transform mapOrigin;
    [Header("다음 웨이브까지의 시간")]
    [SerializeField]
    private float waveTime;

    private BattleInfo battleInfo = null;

    private EnemyInfo waveInfo;
    private List<EnemyInfo> nextWaveInfo = new List<EnemyInfo>();
    private List<GameObject> waveEnemys = new List<GameObject>();
    private GameObject enemy;

    private int currentWave = 1;

    private Vector3 createPos = Vector3.zero;

    private WaitForSeconds waveWait;

    private void Start()
    {
        battleInfo = GetComponent<BattleInfo>();
        waveWait = new WaitForSeconds(waveTime);
        GameManager.Instance.MapCenter = mapCenter.position;
        GameManager.Instance.MapInfo = battleInfo.mapInfo;
        GameManager.Instance.MapOrigin = mapOrigin.position;
        print(GameManager.Instance.MapOrigin);
        EventManager.Instance.OnBattleStart += BattleStart;
    }

    void BattleStart()
    {
        StartCoroutine(Battle());
    }

    IEnumerator Battle()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            if (!battleInfo.enemyInfo.Count.Equals(0))
            {
                CreateWave();
                yield return new WaitUntil(() => { return GameManager.Instance.AllEnemyCount.Equals(0); });
            }
            else
            {
                break;
            }
            yield return waveWait;
        }

        OnBattleEnd();
    }

    void CheckNextWave()
    {
        for (int i = battleInfo.enemyInfo.Count - 1; i >= 0; i--)
        {
            if (battleInfo.enemyInfo[i].waveInfo.Equals(currentWave))
            {
                waveInfo = battleInfo.enemyInfo[i];
                nextWaveInfo.Add(waveInfo);
                battleInfo.enemyInfo.Remove(waveInfo);
            }
        }

        currentWave++;
    }

    void CreateWave()
    {
        nextWaveInfo.Clear();
        CheckNextWave();
        if (!nextWaveInfo.Count.Equals(0))
        {
            for (int i = nextWaveInfo.Count - 1; i >= 0; i--)
            {
                CreateEnemy(nextWaveInfo[i]);
            }
        }
    }

    void CreateEnemy(EnemyInfo wave)
    {
        waveEnemys.Clear();

        for (int i = 0; i < wave.enemyCount.Count; i++)
        {
            createPos.x = mapOrigin.position.x + wave.enemyCount[i].x - 1;
            createPos.y = mapOrigin.position.y + wave.enemyCount[i].y - 1;
            enemy = PoolManager.Instance.GetQueue(wave.enemytype);
            enemy.transform.position = createPos;

            waveEnemys.Add(enemy);
        }
    }

    void OnBattleEnd()
    {
        EventManager.Instance.BattleEnd();
        Destroy(this);
    }

    private void OnDisable()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.OnBattleStart -= BattleStart;
        }
    }
}
