using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;
using UnityEngine.Serialization;

[RequireComponent(typeof(AnimationHandler))]
public class PlayerController : SceneOnlyManager<PlayerController>
{
    private readonly int requestExp = 100;
    private Rigidbody2D Rigidbody2D;
    private PlayerInputHandler PlayerInputHandler;
    public PlayerStatManager PlayerStats { get; private set; }
    private TargetingSystem TargetingSystem;
    protected AnimationHandler animationHandler;

    [SerializeField] private Transform weaponPivot;
    [SerializeField] private WeaponHandler weaponPrefab;
    private WeaponHandler weaponHandler;

    public Vector2 LookDirection { get; private set; } = Vector2.right;


    private bool isMove = false;
    private bool isAttack = true;
    private bool isDead = false;

    private float timeSinceLastAttack = float.MaxValue;
    private float rotateSpeed = 10.0f;


    public int PlayerLevel { get; private set; }
    public int Exp         { get; private set; }

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        PlayerInputHandler = GetComponent<PlayerInputHandler>();
        PlayerStats = GetComponent<PlayerStatManager>();
        TargetingSystem = GetComponent<TargetingSystem>();
        animationHandler = GetComponent<AnimationHandler>();

        if (weaponPrefab != null)
            weaponHandler = Instantiate(weaponPrefab, weaponPivot); //???????
        else
            weaponHandler = GetComponentInChildren<WeaponHandler>();

        weaponHandler.Init(this);
    }

    private void Start()
    {
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        Vector2 moveDir = PlayerInputHandler.moveInput;
        float   moveSpd = PlayerStats.GetFinalValue(StatType.MoveSpeed);
        Rigidbody2D.velocity = moveDir * moveSpd;
        animationHandler.Move(moveDir * moveSpd);
        bool wasMoving = isMove;
        isMove = moveDir.magnitude > 0.01f;

        if (wasMoving != isMove)
        {
            StateChanged(isMove);
        }
    }

    private void Update()
    {
        if (isDead) return;

        // 테스트용 코드
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Debug.Log("F1 : 트리플 샷");
            SkillManager.Instance.SelectSkill(1);
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            Debug.Log("F2 : 백 샷");
            SkillManager.Instance.SelectSkill(2);
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            Debug.Log("F3 : 사이드 샷");
            SkillManager.Instance.SelectSkill(3);
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            Debug.Log("F4 : 공격력 업");
            SkillManager.Instance.SelectSkill(101);
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            Debug.Log("F5 : 공격속도 업");
            SkillManager.Instance.SelectSkill(102);
        }

        if (Input.GetKeyDown(KeyCode.F6))
        {
            Debug.Log("F6 : 이동속도 업");
            SkillManager.Instance.SelectSkill(103);
        }

        GameObject findTarget = TargetingSystem.FindTarget();
        if (findTarget == null)
            return;
        LookDirection = (findTarget.transform.position - transform.position).normalized;
        HandleAttackDelay();
        float curHp = PlayerStats.GetFinalValue(StatType.CurrentHp);
        if (curHp <= 0)
        {
            isDead = true;
            animationHandler.Dead();
        }

        if (isAttack)
        {
            Rotate(LookDirection);
        }
    }

    private void HandleAttackDelay()
    {
        if (weaponHandler == null)
            return;

        float attackSpd = PlayerStats.GetFinalValue(StatType.AttackSpd);
        if (timeSinceLastAttack <= attackSpd)
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        if (isAttack && timeSinceLastAttack > attackSpd)
        {
            timeSinceLastAttack = 0;
            //여기서 이제 앵글값을 준다.
            foreach (ISkill skill in SkillManager.Instance.SelectedSKills)
            {
                if (skill is IAngleArrowSkill arrowSkill)
                {
                    foreach (var angle in arrowSkill.GetAttackAngles())
                    {
                        Attack(angle);
                    }
                }
            }

            Attack();
        }
    }

    private void Attack(float angle = 0)
    {
        weaponHandler.Attack(angle);
    }

    private void StateChanged(bool _isMove)
    {
        if (_isMove)
        {
            Debug.Log("이동시작");
            isMove = true;
            isAttack = false;
        }
        else
        {
            Debug.Log("이동 종료");
            isMove = false;
            isAttack = true;
            timeSinceLastAttack = 0; // 공격 지연 시간 초기화
        }
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ   = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool  isLeft = Mathf.Abs(rotZ) > 90f;

        if (weaponPivot != null)
        {
            weaponPivot.rotation = Quaternion.Lerp(weaponPivot.rotation, Quaternion.Euler(0, 0, rotZ), Time.deltaTime * rotateSpeed);
        }

        weaponHandler?.Rotate(isLeft);
    }


    public void AddExp(int exp)
    {
        Exp += exp;
        while (Exp > requestExp)
        {
            Exp -= requestExp;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        PlayerLevel++;
    }
}