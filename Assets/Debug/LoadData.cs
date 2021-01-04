#define NOTCOMPLETED

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 왜 "data.Length - 1" ?
/*
csv 파일은 마지막에 빈 줄이 하나 있어요.
그거까지 읽으면 대참사가 일어날 것
*/


public class LoadData : MonoBehaviour
{
#if !NOTCOMPLETED
    [Header("실재 데이터가 시작되는 줄 위치")]
    [SerializeField] private int dataStartLine = 1;


    void Start()
    {
        TextAsset balanceData = Resources.Load<TextAsset>("GameBalance/BalanceData");
        if (balanceData == null)
        {
            UnityEditor.EditorUtility.DisplayDialog("GIGDC_TTR", "파일 로드 실패\n\rResources/GameBalance 안에 BalanceData.csv 파일이 있는지 확인하십시오.", "닫기");
            Debug.LogError("!!!BalanceData is NULL!!!");
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            string[] data = balanceData.text.Split(new char[] { '\n' });

            Debug.Log(data.Length);

            for(int i = dataStartLine - 1; i < data.Length - 1; ++i)
            {
                string[] row = data[i].Split(new char[] { ',' });
                BalanceData bData = new BalanceData();
                int.TryParse(row[0], out bData.id); // 데이터가 있으면 int 로 변환해서 bData.id 로 넣음

            }
        }
        
    }
#endif
}
