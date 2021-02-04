using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingBullet : Bullet
{
    Transform target;

    [Header("추격 속도")]
    [SerializeField] [Range(1f, 4f)]
    float moveSpeed = 3f;
    
    
    [Header("근접 거리")]
    [SerializeField]
    [Range(0f, 3f)]
    float contactDistance = 1f;
    
    
    public override void InsertQueue()
    {
        PoolManager.Instance.InsertQueue(this.gameObject, PoolManager.BulletType.FOLLOW);
    }

    private void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        FollowTarget();
    }

    void FollowTarget() 
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }
}
