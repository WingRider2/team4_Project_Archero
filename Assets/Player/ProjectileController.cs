using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;

    private WeaponHandler weaponHandler;//���� ���� 
    private ObjectPool objectPool;
    private float currentDuration;//���ð�?
    private Vector2 direction; //����
    private bool isReady; //�߻��غ� Ȯ��
    private Transform pivot; //�߻���ġ

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
        //�ӵ� ����
        _rigidbody.velocity = direction * weaponHandler.Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision) //�浹 ó��
    {

    }


    public void Init(Vector2 direction, WeaponHandler weaponHandler , ObjectPool objectPool)
    {

        this.weaponHandler = weaponHandler;//�ڵ鷯 ������ �޾�
        this.objectPool = objectPool;
        this.direction = direction;
        currentDuration = 0; 
        transform.localScale = Vector3.one * weaponHandler.BulletSize;//�߻�ü ũ�� ���� �⺻������one
        spriteRenderer.color = weaponHandler.ProjectileColor;//�߻�ü ��������

        transform.right = this.direction;

        if (this.direction.x < 0) //�߻� ���⿡ ���� ����ü�� ���� ����
            pivot.localRotation = Quaternion.Euler(180, 0, 0);
        else
            pivot.localRotation = Quaternion.Euler(0, 0, 0);

        isReady = true;//�߻� �غ� �Ϸ�
    }


}
