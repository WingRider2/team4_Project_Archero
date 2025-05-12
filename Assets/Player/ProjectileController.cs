using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileController : MonoBehaviour, IPoolObject
{
    [SerializeField] PoolType poolType;
    [SerializeField] private int poolSize = 30;
    public GameObject GameObject => gameObject;
    public PoolType   PoolType   => poolType;
    public int        PoolSize   => poolSize;

    private WeaponHandler weaponHandler; //무기 정보 
    private Rigidbody2D _rigidbody;
    private SpriteRenderer spriteRenderer;

    public bool fxOnDestory = true;


    //임시
    public AttackType attackType;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) //충돌 처리
    {
        if (collision.CompareTag("Enemy"))
        {
            // 충돌 처리
            _rigidbody.velocity = Vector3.zero; // 속도 정보 제거
            ObjectPoolManager.Instance.ReturnObject(this);
        }

        else if (collision.CompareTag("Obstacle"))
        {
            // 벽면 혹은 장애물
            // 후에 벽에서 팅기는 거 추가 대비
            _rigidbody.velocity = Vector3.zero; // 속도 정보 제거
            ObjectPoolManager.Instance.ReturnObject(this);
        }
    }

    public void Launch(Vector2 direction, float speed)
    {
        Debug.Log("발사");
        _rigidbody.velocity = direction.normalized * speed;
    }
}