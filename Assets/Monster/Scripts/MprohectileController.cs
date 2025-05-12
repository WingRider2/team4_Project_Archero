using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MprohectileController : MonoBehaviour
{
    private MWeaponHandler mWeaponHandler;
    private float currentDuration;
    private Vector2 direction;
    private bool isReady;  

    private Rigidbody2D rb;
    private void Awake()
    {
       rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (!isReady) return;
        currentDuration += Time.deltaTime;
        if (currentDuration > mWeaponHandler.duration) { 
            Destroy(this.gameObject);
        }
        rb.velocity = direction * mWeaponHandler.speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }
    public void Init(Vector2 dir,MWeaponHandler weaponHandler)
    {
        mWeaponHandler = weaponHandler;
        direction = dir;
        currentDuration = 0;
        transform.right = this.direction;
        isReady= true;
    }
}
