using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum GroundType
{
    NORMAL,
    ICE
}

public class GroundScript : MonoBehaviour
{
    private SpriteRenderer sprRend;
    [Header("땅의 타입")]
    public GroundType groundType = GroundType.NORMAL;

    private void Start()
    {
        sprRend = GetComponent<SpriteRenderer>();
    }

    public void Warning(float warningDur)
    {
        sprRend.DOColor(Color.red, warningDur).OnComplete(() => { sprRend.DOColor(Color.white, warningDur); });
    }
}
