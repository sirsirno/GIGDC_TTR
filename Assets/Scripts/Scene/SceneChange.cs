using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    private void Update()
    {
        if (GameManager.Instance.playerInfo.currentHp <= 0) 
        {
            SceneManager.LoadScene(2);
        }
    }
}
