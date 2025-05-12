using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideArrowSkill : IAngleArrowSkill, ISkill
{
    public int Id { get; set; }
    public string Name { get; set; }
    public SkillType Type { get; set; }
    public float Value { get; set; }

    public SideArrowSkill(SkillData data)
    {
        Id = data.Id;
        Name = data.Name;
        Type = data.Type;
        Value = data.Value;
    }

    public float[] GetAttackAngles()
    {
        float[] angleOffsets = { -90f, 90f };
        return angleOffsets;
    }
}
