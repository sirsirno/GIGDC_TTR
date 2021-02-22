using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;


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

    [SerializeField]
    private TTRCoin coin;
    [SerializeField]
    private Text textCoin;

    private string filePath = "";

    private string jsonString = "";

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

    private void Start()
    {
        coin = new TTRCoin();
        filePath = string.Concat(Application.persistentDataPath, "/", "TTRCoin.txt");
        LoadCoin();
    }

    

    public Vector3 MapCenter { get; set; }  // 카메라가 멈출 맵의 중심
    public Vector3 MapInfo { get; set; }  // 맵에 대한 정보 x,y
    public Vector3 MapOrigin { get; set; }  // 맵의 x=0, y=0이 되는 지점
    [HideInInspector]
    public int AllEnemyCount { get; set; }

    [Header("저장될 게임의 정보")]
    public GameInfo playerInfo;

    public void SaveCoin() 
    {
        jsonString = JsonUtility.ToJson(coin);
        FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate);

        byte[] data = Encoding.UTF8.GetBytes(jsonString);
        fs.Write(data, 0, data.Length);
        fs.Close();
    }

    public void LoadCoin() 
    {
        FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate);
        byte[] data = new byte[fs.Length];
        fs.Read(data, 0, data.Length);
        fs.Close();

        jsonString = Encoding.UTF8.GetString(data);
        coin = JsonUtility.FromJson<TTRCoin>(jsonString);
        UpdateCoin();
    }
    public void UpdateCoin() 
    {
        if (textCoin == null) return;
        textCoin.text = string.Format("{0} 개", coin.coins);
    }
    private void OnApplicationQuit()
    {
        SaveCoin();
    }


}

[Serializable]
public class GameInfo
{
    public float maxHp;
    public float currentHp;
    public float playerDamage;
    
}