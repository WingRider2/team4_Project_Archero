using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class HitPart : MonoBehaviour
{
    public float damagetMultiplier = 1f;

    PlayerStatManager statManager;

    //public System.Action<float, Collision2D> OnHit;
    public void Awake()
    {
        statManager = GetComponentInParent<PlayerStatManager>();
    }

    public void Dead()
    {
    }

    public void Damaged(float dmg)
    {
        statManager.ModifyStatValue(StatType.CurrentHp, StatValueType.Base, -dmg);
        Debug.Log(dmg + "공격");
        if (statManager.GetFinalValue(StatType.CurrentHp) < 0)
        {            
            Dead();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {        
        //OnHit?.Invoke(damagetMultiplier, collision);
    }
}