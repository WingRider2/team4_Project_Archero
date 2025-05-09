using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{   

    private WeaponHandler weaponHandler;//���� ���� 

    private Rigidbody2D _rigidbody;
    private SpriteRenderer spriteRenderer;
    private ObjectPool objectPool;

    public bool fxOnDestory = true;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    public void Init(ObjectPool pool)
    {
        objectPool = pool;
    }
    private void OnTriggerEnter2D(Collider2D collision) //�浹 ó��
    {
        if (collision.CompareTag("Enemy"))
        {
            // �浹 ó��
            _rigidbody.velocity = Vector3.zero; // �ӵ� ���� ����

            if (objectPool != null)
                objectPool.Return(this.gameObject);
            else
            {
                //gameObject.SetActive(false);
            }
        }
        if (collision.CompareTag("Obstacle"))
        {
            // ���� Ȥ�� ��ֹ�
            // �Ŀ� ������ �ñ�� �� �߰� ���
            _rigidbody.velocity = Vector3.zero; // �ӵ� ���� ����

            if (objectPool != null)
                objectPool.Return(this.gameObject);
            else
            {
                //gameObject.SetActive(false);
            }
        }
    }

    public void Launch(Vector2 direction , float speed)
    {
        _rigidbody.velocity = direction.normalized * speed;
    }

}
