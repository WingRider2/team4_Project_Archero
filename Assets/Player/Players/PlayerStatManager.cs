using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public enum StatValueType
{
    Base,
    Buff,
    Equipment
}

public class PlayerStatManager : MonoBehaviour
{
    public readonly Dictionary<StatType, PlayerStat> playerStatDic = new Dictionary<StatType, PlayerStat>();

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        for (int i = 0; i < Enum.GetValues(typeof(StatType)).Length; i++)
        {
            StatType type = (StatType)i;
            playerStatDic[type] = StatFactory(type);
        }
    }

    public void ModifyStatValue(StatType statType, StatValueType valueType, float value)
    {
        switch (valueType)
        {
            case StatValueType.Base:
                playerStatDic[statType].ModifyBaseValue(value);
                break;
            case StatValueType.Buff:
                playerStatDic[statType].ModifyBuffValue(value);
                break;
            case StatValueType.Equipment:
                playerStatDic[statType].ModifyEquipmentValue(value);
                break;
        }

        if (statType == StatType.MaxHp)
        {
            playerStatDic[StatType.CurrentHp].MaxValue = playerStatDic[StatType.MaxHp].FinalValue;
        }
    }

    public float GetFinalValue(StatType statType)
    {
        return playerStatDic[statType].FinalValue;
    }

    private PlayerStat StatFactory(StatType type)
    {
        return type switch
        {
            StatType.MaxHp     => new PlayerStat(type, 5),
            StatType.CurrentHp => new PlayerStat(type, 5),
            StatType.AttackPow => new PlayerStat(type, 5),
            StatType.Defense   => new PlayerStat(type, 5),
            StatType.AttackSpd => new PlayerStat(type, 5, 0.2f, 3f),
            StatType.MoveSpeed => new PlayerStat(type, 5, 2, 8),
            _                  => null
        };
    }
}