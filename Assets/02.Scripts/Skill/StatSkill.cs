using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatSkill : ISkill
{
    public int Id { get; }
    public string Name { get; }
    public SkillType Type { get; }
    public float Value { get; }

    public StatSkillType StatType { get; }
    private PlayerController player;

    public StatSkill(SkillData data, PlayerController player)
    {
        Id = data.Id;
        Name = data.Name;
        Type = data.Type;
        Value = data.Value;
        StatType = data.StatSkillType;
        this.player = player;
    }

    public void ApplyStat()
    {
        switch (StatType)
        {
            case StatSkillType.AttackPow:
                player.PlayerStats.attackPower += (int)Value;
                break;
            case StatSkillType.AttackSpd:
                player.PlayerStats.attackSpeed -= Value;
                break;
            case StatSkillType.MoveSpd:
                player.PlayerStats.moveSpeed += Value;
                break;
        }
    }
}