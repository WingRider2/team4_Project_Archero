using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    private PlayerInputHandler PlayerInputHandler;
    public PlayerStats PlayerStats { get; private set; }
    private TargetingSystem TargetingSystem;

    [SerializeField] private Transform weaponPivot;
    [SerializeField] public WeaponHandler weaponPrefab;
    private WeaponHandler weaponHandler;

    public Vector2 lookDirection = Vector2.right; //���� ����

    // ���� ���� �Ʒ��� ���� ���ݰ� �̵��� ����
    bool isMove = false; // �̵��� �����Ѱ�

    bool isAttack = true; // ������ �����Ѱ�

    // �ִϸ��̼� ����
    // �浹 ó�� => 
    private float timeSinceLastAttack = float.MaxValue;
    private float rotateSpeed = 10.0f;

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        PlayerInputHandler = GetComponent<PlayerInputHandler>();
        PlayerStats = GetComponent<PlayerStats>();
        TargetingSystem = GetComponent<TargetingSystem>();

        if (weaponPrefab != null)
            weaponHandler = Instantiate(weaponPrefab, weaponPivot); //�������
        else
            weaponHandler = GetComponentInChildren<WeaponHandler>();

        weaponHandler.Init(this);
    }

    private void Start()
    {
    }

    private void FixedUpdate()
    {
        Vector2 moveDir = PlayerInputHandler.moveInput;
        Rigidbody2D.velocity = moveDir * PlayerStats.moveSpeed;

        bool wasMoving = isMove;
        isMove = moveDir.magnitude > 0.01f;

        if (wasMoving != isMove)
        {
            StateChanged(isMove);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Debug.Log("F1");
            SkillManager.Instance.SelecteSkill(1);
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            Debug.Log("F2");
            SkillManager.Instance.SelecteSkill(2);
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            Debug.Log("F3");
            SkillManager.Instance.SelecteSkill(3);
        }

        TargetingSystem.findTarget();
        lookDirection = (TargetingSystem.target.transform.position - transform.position).normalized;
        Rotate(lookDirection);

        if (isAttack)
        {
            HandleAttackDelay();
        }
        else
        {
            Debug.Log("�Ƚ����!");
        }
    }

    private void HandleAttackDelay() //���� ��߻� �� �ʿ��� �ð� ���
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
            //여기서 이제 앵글값을 준다.
            bool isSkillAttack = false;
            foreach (ISKill skill in SkillManager.Instance.SelectedSKills)
            {
                var arrowSkill = skill as IAngleArrowSkill;
                if (arrowSkill != null)
                {
                    foreach (var angle in arrowSkill.GetAttackAngles())
                    {
                        isSkillAttack = true;
                        Attack(angle);
                    }
                }
            }

            if (!isSkillAttack)
                Attack();
        }
    }

    private void Attack(float angle = 0)
    {
        weaponHandler.Attack(angle);

        /* ���� �߻� �׽�Ʈ
        weaponHandler.Attack(-30);
        weaponHandler.Attack(0);
        weaponHandler.Attack(30);
        */
    }

    private void StateChanged(bool _isMove)
    {
        if (_isMove)
        {
            Debug.Log("�̵���");
            isMove = true;
            isAttack = false;
        }
        else
        {
            Debug.Log("�����غ�");
            isMove = false;
            isAttack = true;
            timeSinceLastAttack = 0; // ������ ����Ŀ� ����
        }
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ   = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool  isLeft = Mathf.Abs(rotZ) > 90f;

        if (weaponPivot != null)
        {
            //weaponPivot.rotation = Quaternion.Euler(0, 0, rotZ); // ���� ������ �����ش�.
            weaponPivot.rotation = Quaternion.Lerp(weaponPivot.rotation, Quaternion.Euler(0, 0, rotZ), Time.deltaTime * rotateSpeed);
        }

        weaponHandler?.Rotate(isLeft);
    }
}