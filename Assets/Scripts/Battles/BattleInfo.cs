using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInfo : MonoBehaviour
{
    [Header("해당 맵에서 나올 적 정보")]
    public List<EnemyInfo> enemyInfo;

    [Header("맵 크기")]
    public Vector3 mapInfo;
}

[System.Serializable]
public struct EnemyInfo
{
    public PoolManager.EnemyType enemytype;
    public List<Vector2> enemyCount;
    public int waveInfo;
}