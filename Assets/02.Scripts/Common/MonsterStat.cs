using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MonsterStat : BaseStat
{
    public override float FinalValue => Mathf.Clamp(BaseValue + BuffValue, MinValue, MaxValue);


    public MonsterStat(StatType type, float value)
    {
        StatType = type;
        BaseValue = value;
    }

    public override void ModifyBaseValue(float value)
    {
        base.ModifyBaseValue(value);
    }

    public override void ModifyBuffValue(float value)
    {
        base.ModifyBuffValue(value);
    }

    public void ReduceStatInReversePriority(float value)
    {
        
    }

    public override void ClampValues()
    {
    }
}