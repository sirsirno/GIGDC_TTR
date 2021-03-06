using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class BlinkTxt : MonoBehaviour
{
    [SerializeField]
    private Text startTxt = null;

    // Update is called once per frame
    void Start()
    {
        startTxt.DOColor(new Color(1f, 1f, 1f, 10f), 0.8f).SetLoops(-1, LoopType.Yoyo);
        //GetComponent<MeshRenderer>().material.DOFade(0, 1).SetLoops(-1, LoopType.Yoyo);
        //GetComponent<SpriteRenderer>().material.DOFade(0, 1).SetLoops(-1, LoopType.Yoyo);
    }
    private void Update()
    {
        if (Input.anyKeyDown) 
        {
            SceneManager.LoadScene(1);
        }
    }
}
