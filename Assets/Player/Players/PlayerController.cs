using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : SceneOnlyManager<PlayerController>
{
    private readonly int requestExp = 100;
    private Rigidbody2D rgid2D;
    private PlayerInputHandler playerInputHandler;
    private TargetingSystem targetingSystem;
    private AnimationHandler animationHandler;
    [SerializeField] private Transform weaponPivot;
    [SerializeField] private WeaponHandler weaponPrefab;
    private WeaponHandler weaponHandler;


    // ???? ???? ????? ???? ????? ????? ????
    private bool isMove = false;  // ????? ???????
    private bool isAttack = true; // ?????? ???????

    private float timeSinceLastAttack = float.MaxValue;
    private float rotateSpeed = 10.0f;

    public Vector2           LookDirection     { get; private set; } = Vector2.right; //???? ????
    public PlayerStatManager PlayerStatManager { get; private set; }
    public int               PlayerLevel       { get; private set; }
    public int               Exp               { get; private set; }

    protected override void Awake()
    {
        rgid2D = GetComponent<Rigidbody2D>();
        playerInputHandler = GetComponent<PlayerInputHandler>();
        PlayerStatManager = GetComponent<PlayerStatManager>();
        targetingSystem = GetComponent<TargetingSystem>();
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
        Vector2 moveDir = playerInputHandler.moveInput;
        float   moveSpd = PlayerStatManager.GetFinalValue(StatType.MoveSpeed);
        rgid2D.velocity = moveDir * moveSpd;
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

        GameObject findTarget = targetingSystem.FindTarget();
        if (findTarget == null)
            return;
        LookDirection = (findTarget.transform.position - transform.position).normalized;
        Rotate(LookDirection);

        if (isAttack)
        {
            HandleAttackDelay();
        }
        else
        {
            Debug.Log("이동중");
        }
    }

    private void HandleAttackDelay() //???? ???? ?? ????? ?ð? ???
    {
        if (weaponHandler == null)
            return;
        float attackSpd = PlayerStatManager.GetFinalValue(StatType.MoveSpeed);
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

        /* ???? ??? ????
        weaponHandler.Attack(-30);
        weaponHandler.Attack(0);
        weaponHandler.Attack(30);
        */
    }

    private void StateChanged(bool isMove)
    {
        if (isMove)
        {
            Debug.Log("이동시작");
            this.isMove = true;
            isAttack = false;
        }
        else
        {
            Debug.Log("이동 종료");
            this.isMove = false;
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
            //weaponPivot.rotation = Quaternion.Euler(0, 0, rotZ); // ???? ?????? ???????.
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

    protected override void OnDestroy()
    {
    }
}