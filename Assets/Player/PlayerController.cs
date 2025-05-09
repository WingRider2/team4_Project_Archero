using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    private PlayerInputHandler PlayerInputHandler;
    private PlayerStats PlayerStats;

    [SerializeField] private Transform weaponPivot;
    [SerializeField] public WeaponHandler weaponPrefab;
    private WeaponHandler weaponHandler;
    // 이동

    // 상태 제어 아래를 통해 공격과 이동을 제어
    bool isMove = false;// 이동이 가능한가
    bool isAttack = true; // 공격이 가능한가
                    // 애니메이션 동작
                    // 충돌 처리 => 

    // 플레이거 공격 하는 것또한 확장성을 있어야함
    // 플레이어 

    // 투사체 관리

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        PlayerInputHandler = GetComponent<PlayerInputHandler>();
        PlayerStats = GetComponent<PlayerStats>();

        if (weaponPrefab != null)
            weaponHandler = Instantiate(weaponPrefab, weaponPivot); //무기생성
        else
            weaponHandler = GetComponentInChildren<WeaponHandler>();
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
        if (isAttack) { Attack(); }
    }
    private void Attack()
    {
        weaponHandler.Attack();
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
        }
    }
}
