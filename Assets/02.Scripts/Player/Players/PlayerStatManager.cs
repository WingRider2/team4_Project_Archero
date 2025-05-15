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
        var playerData = TableManager.Instance.GetTable<PlayerTable>().GetDataByID(1);
        for (int i = 0; i < playerData.statData.Count; i++)
        {
            StatType type     = playerData.statData[i].StatType;
            float    value    = playerData.statData[i].Value;
            var      statData = StatFactory(type, value);

            playerStatDic[type] = statData;
        }
    }

    public void IncreaseStatValue(StatType statType, StatValueType valueType, float value)
    {
        switch (valueType)
        {
            case StatValueType.Base:
                playerStatDic[statType].IncreaseBaseStat(value);
                break;
            case StatValueType.Buff:
                playerStatDic[statType].ApplyBuffStat(value);
                break;
            case StatValueType.Equipment:
                playerStatDic[statType].IncreaseEquipmentValue(value);
                break;
        }

        if (statType == StatType.MaxHp)
        {
            playerStatDic[StatType.CurrentHp].MaxValue = playerStatDic[StatType.MaxHp].FinalValue;
        }
        else if (statType == StatType.CurrentHp)
        {
            PlayerController.Instance.HpBarUI.UpdateFill(playerStatDic[StatType.CurrentHp].FinalValue, playerStatDic[StatType.MaxHp].FinalValue);
        }
    }

    public void DecreaseStatValue(StatType statType, StatValueType valueType, float value)
    {
        if (value < 0)
            value *= -1;
        switch (valueType)
        {
            case StatValueType.Base:
                playerStatDic[statType].DecreaseBaseValue(value);
                break;
            case StatValueType.Buff:
                playerStatDic[statType].DecreaseBuffValue(value);
                break;
            case StatValueType.Equipment:
                playerStatDic[statType].DecreaseEquipmentValue(value);
                break;
        }

        if (statType == StatType.MaxHp)
        {
            playerStatDic[StatType.CurrentHp].MaxValue = playerStatDic[StatType.MaxHp].FinalValue;
        }
        else if (statType == StatType.CurrentHp)
        {
            PlayerController.Instance.HpBarUI.UpdateFill(playerStatDic[StatType.CurrentHp].FinalValue, playerStatDic[StatType.MaxHp].FinalValue);
        }
    }

    public void AllDecreaseStatValue(StatType statType, float value)
    {
        playerStatDic[statType].DecreaseAllValue(value);
        if (statType == StatType.MaxHp)
        {
            playerStatDic[StatType.CurrentHp].MaxValue = playerStatDic[StatType.MaxHp].FinalValue;
        }
        else if (statType == StatType.CurrentHp)
        {
            PlayerController.Instance.HpBarUI.UpdateFill(playerStatDic[StatType.CurrentHp].FinalValue, playerStatDic[StatType.MaxHp].FinalValue);
        }
    }

    public float GetFinalValue(StatType statType)
    {
        return playerStatDic[statType].FinalValue;
    }

    private PlayerStat StatFactory(StatType type, float value)
    {
        return type switch
        {
            StatType.MaxHp     => new PlayerStat(type, value),
            StatType.CurrentHp => new PlayerStat(type, value),
            StatType.AttackPow => new PlayerStat(type, value),
            StatType.Defense   => new PlayerStat(type, value),
            StatType.AttackSpd => new PlayerStat(type, value, 0.2f, 3f),
            StatType.MoveSpeed => new PlayerStat(type, value, 2, 8),
            _                  => null
        };
    }
}