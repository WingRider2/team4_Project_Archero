using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class HitPart : MonoBehaviour
{
    PlayerStatManager statManager;

    //public System.Action<float, Collision2D> OnHit;
    public void Awake()
    {
        statManager = GetComponentInParent<PlayerStatManager>();
    }

    public void Damaged(float dmg)
    {
        statManager.AllDecreaseStatValue(StatType.CurrentHp, dmg);
        Debug.Log(dmg + "공격");
        if (statManager.GetFinalValue(StatType.CurrentHp) < 0)
        {
            PlayerController.Instance.Dead();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //OnHit?.Invoke(damagetMultiplier, collision);
    }
}