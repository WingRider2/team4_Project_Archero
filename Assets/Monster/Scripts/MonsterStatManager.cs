using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatManager : MonoBehaviour
{
    public readonly Dictionary<StatType, MonsterStat> monsterStatDic = new Dictionary<StatType, MonsterStat>();

    public void Initialize(MonsterData monsterData)
    {
        for (int i = 0; i < monsterData.Stats.Count; i++)
        {
            var stat = monsterData.Stats[i];
            monsterStatDic[stat.StatType] = StatFactory(stat.StatType, stat.Value);
        }
    }

    public void IncreaseStatValue(StatType statType, StatValueType valueType, float value)
    {
        switch (valueType)
        {
            case StatValueType.Base:
                monsterStatDic[statType].IncreaseBaseStat(value);
                break;
            case StatValueType.Buff:
                monsterStatDic[statType].IncreaseBuffStat(value);
                break;
        }

        if (statType == StatType.MaxHp)
        {
            monsterStatDic[StatType.CurrentHp].MaxValue = monsterStatDic[StatType.MaxHp].FinalValue;
        }
    }

    public void DecreaseStatValue(StatType statType, StatValueType valueType, float value)
    {
        switch (valueType)
        {
            case StatValueType.Base:
                monsterStatDic[statType].DecreaseBaseValue(value);
                break;
            case StatValueType.Buff:
                monsterStatDic[statType].DecreaseBuffValue(value);
                break;
        }

        if (statType == StatType.MaxHp)
        {
            monsterStatDic[StatType.CurrentHp].MaxValue = monsterStatDic[StatType.MaxHp].FinalValue;
        }
    }

    public void AllDecreaseStatValue(StatType statType, float value)
    {
        monsterStatDic[statType].DecreaseAllValue(value);
        if (statType == StatType.MaxHp)
        {
            monsterStatDic[StatType.CurrentHp].MaxValue = monsterStatDic[StatType.MaxHp].FinalValue;
        }
    }

    public float GetFinalValue(StatType statType)
    {
        return monsterStatDic[statType].FinalValue;
    }

    private MonsterStat StatFactory(StatType type, float value)
    {
        return new MonsterStat(type, value);
    }
}