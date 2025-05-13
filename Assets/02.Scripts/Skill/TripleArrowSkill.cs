using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TripleArrowSkill : IAngleArrowSkill, ISkill
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Info { get; set; }
    public SkillType Type { get; set; }
    public float Value { get; set; }

    public Sprite SkillIcon { get; private set; }

    public TripleArrowSkill(SkillData data)
    {
        Id = data.Id;
        Name = data.Name;
        Info = data.Info;
        Type = data.Type;
        Value = data.Value;
        SkillIcon = data.SkillIcon;

        float[] angleOffsets = { -15f, 15f };
    }

    public float[] GetAttackAngles()
    {
        float[] angleOffsets = { -15f, 15f };
        return angleOffsets;
    }
}