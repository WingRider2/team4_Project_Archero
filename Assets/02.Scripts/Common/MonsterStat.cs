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

    public void DecreaseAllValue(float value)
    {
        float remain = value;

        remain = DecreaseBuffValue(remain);
        remain = DecreaseBaseValue(remain);
    }
}