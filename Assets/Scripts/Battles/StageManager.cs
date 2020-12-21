using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [Header("나오는 스테이지들 순서대로 넣기")]
    [SerializeField]
    private List<GameObject> stages;
    public int currentIdx;

    private void Start()
    {
        currentIdx = 0;
        EventManager.Instance.OnBattleEnd += OnBattleEnd;
    }

    void OnBattleEnd()
    {
        if (currentIdx < stages.Count - 1)
        {
            stages[currentIdx + 1].SetActive(true);
            currentIdx++;
        }
    }
}
