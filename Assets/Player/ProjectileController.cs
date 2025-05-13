using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileController : MonoBehaviour, IPoolObject
{
    [SerializeField] private PoolType poolType;
    [SerializeField] private int poolSize = 30;
    public GameObject GameObject => gameObject;
    public PoolType PoolType => poolType;
    public int PoolSize => poolSize;

    private WeaponHandler weaponHandler; //무기 정보 
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;

    public bool fxOnDestory = true;

    // 디버프 관련 정보
    public DebuffType debuffType = DebuffType.None;
    public float debuffDPS = 1f;
    public float debuffDuration = 5f;
    public SkillType skillType;


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
        if (collision.CompareTag("Enemy") && collision.TryGetComponent(out MonsterBase monster))
        {
<<<<<<< Updated upstream
            collision.gameObject.GetComponent<MonsterBase>().Damaged(10);
=======
            // 스킬타입이 Attack이고, 디버프가 있을 경우에만 적용
            if (skillType == SkillType.Attack && debuffType != DebuffType.None)
            {
                monster.ApplyDebuff(debuffType, debuffDPS, debuffDuration);
            }

            // 충돌 처리
>>>>>>> Stashed changes
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