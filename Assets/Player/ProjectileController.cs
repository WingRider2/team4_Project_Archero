using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;

    private WeaponHandler weaponHandler;//무기 정보 
    private ObjectPool objectPool;
    private float currentDuration;//대기시간?
    private Vector2 direction; //방향
    private bool isReady; //발사준비 확인
    private Transform pivot; //발사위치

    private Rigidbody2D _rigidbody;
    private SpriteRenderer spriteRenderer;

    public bool fxOnDestory = true;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        pivot = transform.GetChild(0);
    }

    private void Update()
    {
        if (!isReady)
        {
            return;
        }

        currentDuration += Time.deltaTime;

        if (currentDuration > weaponHandler.Duration)
        {
            
        }
        //속도 설정
        _rigidbody.velocity = direction * weaponHandler.Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision) //충돌 처리
    {

    }


    public void Init(Vector2 direction, WeaponHandler weaponHandler , ObjectPool objectPool)
    {

        this.weaponHandler = weaponHandler;//핸들러 정보를 받아
        this.objectPool = objectPool;
        this.direction = direction;
        currentDuration = 0; 
        transform.localScale = Vector3.one * weaponHandler.BulletSize;//발사체 크기 조정 기본사이즈one
        spriteRenderer.color = weaponHandler.ProjectileColor;//발사체 색상조정

        transform.right = this.direction;

        if (this.direction.x < 0) //발사 방향에 따른 투사체의 방향 조정
            pivot.localRotation = Quaternion.Euler(180, 0, 0);
        else
            pivot.localRotation = Quaternion.Euler(0, 0, 0);

        isReady = true;//발사 준비 완료
    }


}
