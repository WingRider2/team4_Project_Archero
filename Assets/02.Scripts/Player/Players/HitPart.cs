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
        if (statManager.GetFinalValue(StatType.CurrentHp) < 0)
        {
            PlayerController.Instance.Dead();
        }
    }
}