using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class WeaponHandler : MonoBehaviour
{
    [Header("Attack Info")]
    [SerializeField] private float delay = 1f; //공격속도 => 재발사 시간
    public float Delay { get => delay; set => delay = value; }

    [SerializeField] private float weaponSize = 1f; //무기크기
    public float WeaponSize { get => weaponSize; set => weaponSize = value; }

    [SerializeField] private float power = 1f; //무기 피해?
    public float Power { get => power; set => power = value; }

    [SerializeField] private float speed = 1f; //투사체 속도
    public float Speed { get => speed; set => speed = value; }

    [SerializeField] private float attackRange = 10f; //공격범위
    public float AttackRange { get => attackRange; set => attackRange = value; }

    public LayerMask target;//공격 명중시 사용예정?

    [Header("Knock Back Info")]
    [SerializeField] private bool isOnKnockback = false;
    public bool IsOnKnockback { get => isOnKnockback; set => isOnKnockback = value; }

    [SerializeField] private float knockbackPower = 0.1f;
    public float KnockbackPower { get => knockbackPower; set => knockbackPower = value; }

    [SerializeField] private float knockbackTime = 0.5f;
    public float KnockbackTime { get => knockbackTime; set => knockbackTime = value; }
        
    [Header("Ranged Attack Data")]

    [SerializeField] private int bulletIndex;//투사체번호
    public int BulletIndex { get { return bulletIndex; } }

    [SerializeField] private float bulletSize = 1; //투사체 크기
    public float BulletSize { get { return bulletSize; } }

    [SerializeField] private float duration;//투사체발사 지연시간
    public float Duration { get { return duration; } }

    [SerializeField] private float spread;//?
    public float Spread { get { return spread; } }

    [SerializeField] private int numberofProjectilesPerShot;//발사체당 개수?
    public int NumberofProjectilesPerShot { get { return numberofProjectilesPerShot; } }

    [SerializeField] private float multipleProjectilesAngle;//발사체 2개 사이의 발사 각도
    public float MultipleProjectilesAngle { get { return multipleProjectilesAngle; } }

    [SerializeField] private Color projectileColor;//투사체 색상
    public Color ProjectileColor { get { return projectileColor; } }
    // 무기에 대해서

    // 오브젝트 풀은 후에 매니저를 통해서관리
    ObjectPool objectPool;
    [SerializeField]Transform firePoint;

    private void Awake()
    {
        objectPool= FindObjectOfType<ObjectPool>();
    }

    public void Attack()
    {
        //각도 조절
        GameObject arrow = objectPool.Get();
        arrow.transform.position = firePoint.position;
        arrow.transform.rotation = firePoint.rotation;
        //bullet.GetComponent<Projectile>().Launch(direction);
    }
}
