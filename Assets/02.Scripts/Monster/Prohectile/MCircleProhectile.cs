using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using System;

public class MCircleProhectile : MprohectileController
{
    // Start is called before the first frame update
    Transform Target;
    public float radius = 2f;
    private float angle;
    public Action onFinished;

    protected override void Start()
    {
        base.Start();
    }


    // Update is called once per frame
    protected override void Update()
    {
        if (!isReady) return;
        currentDuration += Time.deltaTime;


        if (currentDuration > mWeaponHandler.duration)
        {
            mPoolManager.ReturnObject(this);
            onFinished?.Invoke();
            return;
        }

        angle += mWeaponHandler.speed * 50f * Time.deltaTime;
        float   rad    = angle * Mathf.Deg2Rad;
        Vector2 offset = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * radius;
        if (Target == null)
            return;
        Vector2 targetPostion = (Vector2)Target.position + offset + new Vector2(0, 0.5f);
        rb.MovePosition(targetPostion);
    }

    public void Init(Vector2 dir, MWeaponHandler weaponHandler, float startAngle)
    {
        this.angle = startAngle;
        mWeaponHandler = weaponHandler;
        Target = mWeaponHandler.transform;
        currentDuration = 0f;

        transform.right = this.direction;
        isReady = true;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        collision.gameObject.GetComponent<HitPart>()?.Damaged(3);
        onFinished?.Invoke();
        mPoolManager.ReturnObject(this);
    }
}