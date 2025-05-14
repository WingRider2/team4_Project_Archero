using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : BaseStat
{
    public float EquipmentValue { get; private set; }

    public override float FinalValue => Mathf.Clamp(BaseValue + BuffValue + EquipmentValue, MinValue, MaxValue);

    public PlayerStat(StatType type, float baseValue, float min = 0, float max = int.MaxValue)
    {
        StatType = type;
        BaseValue = baseValue;
        MinValue = min;
        MaxValue = max;
    }

    public void IncreaseEquipmentValue(float value)
    {
        EquipmentValue += value;
    }

    public float DecreaseEquipmentValue(float value)
    {
        float decreaseAmount = Mathf.Min(EquipmentValue, value);
        EquipmentValue -= decreaseAmount;
        return value - decreaseAmount;
    }

    public void DecreaseAllValue(float value)
    {
        float remain = value;

        remain = DecreaseEquipmentValue(remain);
        remain = DecreaseBuffValue(remain);
        remain = DecreaseBaseValue(remain);
    }
}