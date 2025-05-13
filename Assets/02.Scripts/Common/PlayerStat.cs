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

    public override void ModifyBaseValue(float value)
    {
        base.ModifyBaseValue(value);
    }

    public override void ModifyBuffValue(float value)
    {
        base.ModifyBuffValue(value);
    }

    /// <summary>
    /// 직접 호출해서 사용하면 X 반드시 StatManager를 통해 호출
    /// </summary>
    /// <param name="value"></param>
    public void ModifyEquipmentValue(float value)
    {
        EquipmentValue += value;
        if (EquipmentValue < 0)
        {
            ModifyBuffValue(EquipmentValue);
            EquipmentValue = Mathf.Max(EquipmentValue, 0);
        }

        TriggerEvent(EquipmentValue);
    }

    /// <summary>
    /// 필요없을듯..?
    /// </summary>
    public override void ClampValues()
    {
        BaseValue = Mathf.Clamp(BaseValue, MinValue, MaxValue);
        BuffValue = Mathf.Clamp(BuffValue, MinValue, MaxValue);
        EquipmentValue = Mathf.Clamp(EquipmentValue, MinValue, MaxValue);
        TriggerEvent(FinalValue);
    }
}