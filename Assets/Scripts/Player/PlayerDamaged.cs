using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamaged : MonoBehaviour, IDamageable
{
    [SerializeField]
    private Image hpImage = null;
    private bool is_Damaged;
    private Shootable shootable;

    [Header("다음 데미지까지 걸리는 시간")]
    [SerializeField]
    private float damageableDur;

    private WaitForSeconds damageWait;
    private WaitForSeconds colorWait;

    private SpriteRenderer sprRend;
    private Color damagedColor = new Color(1f, 1f, 1f, 0.3f);

    void Start()
    {
        hpImage.fillAmount = 1;
        sprRend = GetComponent<SpriteRenderer>();
        damageWait = new WaitForSeconds(damageableDur);
        colorWait = new WaitForSeconds(0.1f);
        StartCoroutine(DamagedControl());
        StartCoroutine(OnDamaged());
    }
    

    IEnumerator DamagedControl()
    {
        while (true)
        {
            yield return new WaitUntil(() => { return is_Damaged; });
            yield return damageWait;
            is_Damaged = false;
        }
    }

    IEnumerator OnDamaged()
    {
        while (true)
        {
            yield return new WaitUntil(() => { return is_Damaged; });

            while (is_Damaged)
            {
                sprRend.color = damagedColor;
                yield return colorWait;
                sprRend.color = Color.white;
                yield return colorWait;
            }
        }
    }

    public void OnDamaged(float damage)
    {
        
        if (is_Damaged) { return; }
        GameManager.Instance.playerInfo.currentHp -= damage;
        is_Damaged = true;
        print("플레이어 현재 체력 : " + GameManager.Instance.playerInfo.currentHp);
        hpImage.fillAmount = GameManager.Instance.playerInfo.currentHp * 0.01f ;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("EnemyAttack")))
        {
            if (other.CompareTag("Lazer"))
            {
                shootable = other.GetComponentInParent<Shootable>();
                OnDamaged(shootable.Damage);
            }
        }
    }
}
