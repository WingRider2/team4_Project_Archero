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

    public Vector2 lookDirection = Vector2.right;//���� ����
    // ���� ���� �Ʒ��� ���� ���ݰ� �̵��� ����
    bool isMove = false;// �̵��� �����Ѱ�
    bool isAttack = true; // ������ �����Ѱ�
                          // �ִϸ��̼� ����
                          // �浹 ó�� => 
    private float timeSinceLastAttack = float.MaxValue;
    // �÷��̰� ���� �ϴ� �Ͷ��� Ȯ�强�� �־����
    // �÷��̾� 

    // ����ü ����

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
    // ���� ���� ȸ�� 
    // ���� ���̺� ��ȸ?
    // ���� ����� ��Ʈ�� ����
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
            Debug.Log("�Ƚ����!");
        }
    }
    private void HandleAttackDelay()//���� ��߻� �� �ʿ��� �ð� ���
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
        weaponHandler.Attack();
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

    private void Rotate(Vector2 direction) //�÷��̾� �¿� ȸ��
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//Rad2Deg�� 1���� //Atan2 y�� x �� ���� ���� ������ ���Ѵ�. ���⿡ *Rad2Deg�� �ϸ� ���� �ٲ��ش�.
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        if (weaponPivot != null)
        {
            weaponPivot.rotation = Quaternion.Euler(0, 0, rotZ); // ���� ������ �����ش�.
        }

        weaponHandler?.Rotate(isLeft);
    }
}
