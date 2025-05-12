using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Pool;

public class WeaponHandler : MonoBehaviour
{
    [Header("Attack Info")]
    [SerializeField] private float delay = 1f; //공격속도 => 재발사 시간
    public float Delay { get => delay; set => delay = value; }

    [SerializeField] private float weaponSize = 1f; //무기크기
    public float WeaponSize { get => weaponSize; set => weaponSize = value; }

    [SerializeField] private float power = 1f; //무기 피해
    public float Power { get => power; set => power = value; }

    [SerializeField] private float speed = 1f; //투사체 속도
    public float Speed { get => speed; set => speed = value; }

    public LayerMask target;//공격 명중시 사용예정?

    [Header("Knock Back Info")]
    [SerializeField] private bool isOnKnockback = false;
    public bool IsOnKnockback { get => isOnKnockback; set => isOnKnockback = value; }

    [SerializeField] private float knockbackPower = 0.1f;
    public float KnockbackPower { get => knockbackPower; set => knockbackPower = value; }

    [SerializeField] private float knockbackTime = 0.5f;
    public float KnockbackTime { get => knockbackTime; set => knockbackTime = value; }


    [SerializeField] private float bulletSize = 1; //투사체 크기
    public float BulletSize { get { return bulletSize; } }
    
    // 무기에 대해서

    // 오브젝트 풀은 후에 매니저를 통해서관리
    ObjectPool objectPool;
    [SerializeField] Transform firePoint;
    [SerializeField] PlayerController player; // 방향 받아오기
    [SerializeField] SpriteRenderer weaponRenderer;

    private void Awake()
    {
        objectPool= FindObjectOfType<ObjectPool>();
        weaponRenderer = GetComponentInChildren<SpriteRenderer>();


    }
    public void Init(PlayerController playerController)
    {
        player = playerController;

    }
    public void Attack()
    {
        //화살의 정보 불러오기
        GameObject arrow = objectPool.Get();
        ProjectileController controller = arrow.GetComponent<ProjectileController>();
        controller.Init(objectPool);

        //화살 발사 각도 조절
        Vector2 direction = player.lookDirection;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;       
        arrow.transform.position = firePoint.position;
        arrow.transform.rotation = Quaternion.Euler(0, 0, angle);

        //여기 쯤에서 추가연산?
        controller.Launch(direction,speed);
    }
    public void Attack(float _angle)
    {
        //화살의 정보 불러오기
        GameObject arrow = objectPool.Get();
        ProjectileController controller = arrow.GetComponent<ProjectileController>();
        controller.Init(objectPool);

        //화살 발사 각도 조절
        Vector2 direction = Quaternion.Euler(0, 0, _angle) * player.lookDirection ;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg ;
        arrow.transform.position = firePoint.position;
        arrow.transform.rotation = Quaternion.Euler(0, 0, angle);

        //여기 쯤에서 추가연산?
        controller.Launch(direction, speed);
    }
    public virtual void Rotate(bool isLeft)
    {
        weaponRenderer.flipY = isLeft;
    }
}
