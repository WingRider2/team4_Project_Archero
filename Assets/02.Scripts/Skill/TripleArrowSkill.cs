using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleArrowSkill : IAngleArrowSkill, ISKill
{
    public int       Id    { get; set; }
    public string    Name  { get; set; }
    public SkillType Type  { get; set; }
    public float     Value { get; set; }

    public TripleArrowSkill(SkillData data)
    {
        Id = data.Id;
        Name = data.Name;
        Type = data.Type;
        Value = data.Value;
    }

    public float[] GetAttackAngles()
    {
        float[] angleOffsets = { -15f, 0f, 15f };
        return angleOffsets;
    }
}