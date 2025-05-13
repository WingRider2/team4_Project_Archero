using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class HitPart : MonoBehaviour
{
    public float damagetMultiplier = 1f;
    PlayerStats stats;
    //public System.Action<float, Collision2D> OnHit;
    public void Awake()
    {
        stats= GetComponentInParent<PlayerStats>();
    }
    public void Dead()
    {

    }
    public void Damaged(float dmg)
    {
        stats.currentHP-=(int)dmg;
        Debug.Log(dmg + "공격");
        if (stats.currentHP < 0) {
            Dead();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
      
        //OnHit?.Invoke(damagetMultiplier, collision);
    }
}
