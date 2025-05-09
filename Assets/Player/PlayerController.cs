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
    // �̵�

    // ���� ���� �Ʒ��� ���� ���ݰ� �̵��� ����
    bool isMove = false;// �̵��� �����Ѱ�
    bool isAttack = true; // ������ �����Ѱ�
                    // �ִϸ��̼� ����
                    // �浹 ó�� => 

    // �÷��̰� ���� �ϴ� �Ͷ��� Ȯ�强�� �־����
    // �÷��̾� 

    // ����ü ����

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        PlayerInputHandler = GetComponent<PlayerInputHandler>();
        PlayerStats = GetComponent<PlayerStats>();

        if (weaponPrefab != null)
            weaponHandler = Instantiate(weaponPrefab, weaponPivot); //�������
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
            Debug.Log("�̵���");
            isMove = true;
            isAttack = false;
        }
        else
        {
            Debug.Log("�����غ�");
            isMove = false;
            isAttack = true;
        }
    }
}
