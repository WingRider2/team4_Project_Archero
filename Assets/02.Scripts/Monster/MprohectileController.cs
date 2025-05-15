using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MprohectileController : MonoBehaviour, IPoolObject
{
    [SerializeField] PoolType poolType;
    [SerializeField] private int poolSize = 20;

    protected MWeaponHandler mWeaponHandler;
    protected float currentDuration;
    protected Vector2 direction;
    protected bool isReady;


    protected Rigidbody2D rb;

    public GameObject GameObject => gameObject;
    public PoolType   PoolType   => poolType;
    public int        PoolSize   => poolSize;

    protected ObjectPoolManager mPoolManager;

    private MonsterBase m_MonsterBase;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        mPoolManager = ObjectPoolManager.Instance;
        m_MonsterBase = mWeaponHandler.GetComponent<MonsterBase>();
    }

    protected virtual void Update()
    {
        if (!isReady) return;
        currentDuration += Time.deltaTime;
        if (currentDuration > mWeaponHandler.duration)
        {
            mPoolManager.ReturnObject(this);
        }

        rb.velocity = direction * mWeaponHandler.speed;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponentInChildren<HitPart>()?.Damaged(m_MonsterBase.MonsterStatManager.GetFinalValue(StatType.AttackPow));

            mPoolManager.ReturnObject(this);
        }

        if (collision.CompareTag("Obstacle"))
        {
            mPoolManager.ReturnObject(this);
        }
    }

    public virtual void Init(Vector2 dir, MWeaponHandler weaponHandler)
    {
        mWeaponHandler = weaponHandler;
        direction = dir;
        currentDuration = 0;
        transform.right = this.direction;
        isReady = true;
    }
}