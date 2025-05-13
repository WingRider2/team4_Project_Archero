using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MprohectileController : MonoBehaviour, IPoolObject
{
    [SerializeField] PoolType poolType;
    [SerializeField] private int poolSize = 20;

    private MWeaponHandler mWeaponHandler;
    private float currentDuration;
    private Vector2 direction;
    private bool isReady;


    private Rigidbody2D rb;

    public GameObject GameObject => gameObject;
    public PoolType   PoolType   => poolType;
    public int        PoolSize   => poolSize;

    private ObjectPoolManager mPoolManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        mPoolManager = ObjectPoolManager.Instance;
    }

    private void Update()
    {
        if (!isReady) return;
        currentDuration += Time.deltaTime;
        if (currentDuration > mWeaponHandler.duration)
        {
            mPoolManager.ReturnObject(this);
        }

        rb.velocity = direction * mWeaponHandler.speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        mPoolManager.ReturnObject(this);
    }

    public void Init(Vector2 dir, MWeaponHandler weaponHandler)
    {
        mWeaponHandler = weaponHandler;
        direction = dir;
        currentDuration = 0;
        transform.right = this.direction;
        isReady = true;
    }
}