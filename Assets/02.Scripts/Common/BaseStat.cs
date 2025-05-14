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

    public void IncreaseBaseStat(float value)
    {
        BaseValue += value;
    }

    public float DecreaseBaseValue(float value)
    {
        float decreaseAmount = Mathf.Min(BaseValue, value);
        BaseValue -= decreaseAmount;
        return value - decreaseAmount;
    }

    public void IncreaseBuffStat(float value)
    {
        BuffValue += value;
    }

    public float DecreaseBuffValue(float value)
    {
        BuffValue -= value;
        return BuffValue;
    }

    protected void TriggerEvent(float updateValue)
    {
        OnValueChanged?.Invoke(updateValue);
        Debug.Log($"Value Changed to {updateValue} Final Value : {FinalValue}");
    }
}