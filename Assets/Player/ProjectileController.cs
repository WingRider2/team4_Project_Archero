using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileController : MonoBehaviour, IPoolObject
{
    [SerializeField] private PoolType poolType;
    [SerializeField] private int poolSize = 30;
    public GameObject GameObject => gameObject;
    public PoolType   PoolType   => poolType;
    public int        PoolSize   => poolSize;

    private WeaponHandler weaponHandler; //무기 정보 
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;

    public bool fxOnDestory = true;


    //임시
    public AttackType attackType;
    ObjectPoolManager mPoolManager;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        mPoolManager = ObjectPoolManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision) //충돌 처리
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<MonsterBase>().Damaged(10);
            rigid.velocity = Vector3.zero; // 속도 정보 제거
            mPoolManager.ReturnObject(this);
        }

        else if (collision.CompareTag("Obstacle"))
        {
            // 벽면 혹은 장애물
            // 후에 벽에서 팅기는 거 추가 대비
            rigid.velocity = Vector3.zero; // 속도 정보 제거
            mPoolManager.ReturnObject(this);
        }
    }

    public void Launch(Vector2 direction, float speed)
    {
        Debug.Log("발사");
        rigid.velocity = direction.normalized * speed;
    }
}