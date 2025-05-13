using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    MaxHp,
    CurrentHp,
    AttackPow,
    Defense,
    AttackSpd,
    MoveSpeed,
}

public abstract class BaseStat
{
    public          StatType StatType   { get; set; }
    public          float    BaseValue  { get; protected set; }
    public          float    BuffValue  { get; protected set; }
    public abstract float    FinalValue { get; }

    public float MinValue = 0;
    public float MaxValue = int.MaxValue;

    public event Action<float> OnValueChanged;

    /// <summary>
    /// 직접 호출해서 사용하면 X 반드시 StatManager를 통해 호출
    /// </summary>
    /// <param name="value"></param>
    public virtual void ModifyBaseValue(float value)
    {
        BaseValue += value;
        if (BaseValue < 0)
        {
            BaseValue = Mathf.Max(BaseValue, 0);
        }

        TriggerEvent(BaseValue);
    }

    /// <summary>
    /// 직접 호출해서 사용하면 X 반드시 StatManager를 통해 호출
    /// </summary>
    /// <param name="value"></param>
    public virtual void ModifyBuffValue(float value)
    {
        BuffValue += value;
        if (BuffValue < 0)
        {
            ModifyBaseValue(BuffValue);
            BuffValue = Mathf.Max(BuffValue, 0);
        }

        TriggerEvent(BuffValue);
    }

    public abstract void ClampValues();

    protected void TriggerEvent(float updateValue)
    {
        OnValueChanged?.Invoke(updateValue);
        Debug.Log($"Value Changed to {updateValue} Final Value : {FinalValue}");
    }
}