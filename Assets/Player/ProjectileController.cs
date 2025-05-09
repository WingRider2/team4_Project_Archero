using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{   

    private WeaponHandler weaponHandler;//公扁 沥焊 

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
    private void OnTriggerEnter2D(Collider2D collision) //面倒 贸府
    {
        if (collision.CompareTag("Enemy"))
        {
            // 面倒 贸府
            _rigidbody.velocity = Vector3.zero; // 加档 沥焊 力芭

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
