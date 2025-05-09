using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    private PlayerInputHandler PlayerInputHandler;
    private PlayerStats PlayerStats;
    private TargetingSystem TargetingSystem;

    [SerializeField] private Transform weaponPivot;
    [SerializeField] public WeaponHandler weaponPrefab;
    private WeaponHandler weaponHandler;

    public Vector2 lookDirection = Vector2.right;//보는 방향
    // 상태 제어 아래를 통해 공격과 이동을 제어
    bool isMove = false;// 이동이 가능한가
    bool isAttack = true; // 공격이 가능한가
                          // 애니메이션 동작
                          // 충돌 처리 => 
    private float timeSinceLastAttack = float.MaxValue;
    private float rotateSpeed = 10.0f;


    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        PlayerInputHandler = GetComponent<PlayerInputHandler>();
        PlayerStats = GetComponent<PlayerStats>();
        TargetingSystem = GetComponent<TargetingSystem>();

        if (weaponPrefab != null)
            weaponHandler = Instantiate(weaponPrefab, weaponPivot); //무기생성
        else
            weaponHandler = GetComponentInChildren<WeaponHandler>();

        weaponHandler.Init(this);
    }
    private void FixedUpdate()
    {
        Vector2 moveDir = PlayerInputHandler.moveInput;
        Rigidbody2D.velocity = moveDir * PlayerStats.moveSpeed;

        bool wasMoving = isMove;
        isMove = moveDir.magnitude > 0.01f;

        if (wasMoving != isMove) {
            StateChanged(isMove);
        }
    }
    private void Update()
    {
        TargetingSystem.findTarget();
        lookDirection = (TargetingSystem.target.transform.position - transform.position).normalized;
        Rotate(lookDirection);

        if (isAttack) {
            HandleAttackDelay();
        }
        else
        {
            Debug.Log("안쏘는중!");
        }
    }
    private void HandleAttackDelay()//무기 재발사 에 필요한 시간 계산
    {
        if (weaponHandler == null)
            return;

        if (timeSinceLastAttack <= weaponHandler.Delay)
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        if (isAttack && timeSinceLastAttack > weaponHandler.Delay)
        {
            timeSinceLastAttack = 0;
            Attack();
        }
    }
    private void Attack()
    {
        weaponHandler.Attack(0);

        /* 각도 발사 테스트
        weaponHandler.Attack(-30);
        weaponHandler.Attack(0);
        weaponHandler.Attack(30);
        */
    }
    private void StateChanged(bool _isMove)
    {
        if (_isMove)
        {
            Debug.Log("이동중");
            isMove = true;
            isAttack = false;
        }
        else
        {
            Debug.Log("공격준비");
            isMove = false;
            isAttack = true;
            timeSinceLastAttack = 0; // 멈춘후 잠시후에 공격
        }
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        if (weaponPivot != null)
        {            
            //weaponPivot.rotation = Quaternion.Euler(0, 0, rotZ); // 무기 방향을 돌려준다.
            weaponPivot.rotation = Quaternion.Lerp(weaponPivot.rotation, Quaternion.Euler(0, 0, rotZ), Time.deltaTime * rotateSpeed);
        }

        weaponHandler?.Rotate(isLeft);
    }
}
