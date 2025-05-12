using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{   
    private WeaponHandler weaponHandler;//무기 정보 
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
    private void OnTriggerEnter2D(Collider2D collision) //충돌 처리
    {
        if (collision.CompareTag("Enemy"))
        {
            // 충돌 처리
            _rigidbody.velocity = Vector3.zero; // 속도 정보 제거

            if (objectPool != null)
                objectPool.Return(this.gameObject);
            else
            {
                //gameObject.SetActive(false);
            }
        }
        if (collision.CompareTag("Obstacle"))
        {
            // 벽면 혹은 장애물
            // 후에 벽에서 팅기는 거 추가 대비
            _rigidbody.velocity = Vector3.zero; // 속도 정보 제거

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
