using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackArrowSkill : IAngleArrowSkill, ISkill
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public SkillType Type { get; private set; }
    public float Value { get; private set; }

    public Sprite SkillIcon { get; private set; }

    public BackArrowSkill(SkillData data)
    {
        Id = data.Id;
        Name = data.Name;
        Type = data.Type;
        Value = data.Value;
        SkillIcon = data.SkillIcon;
    }

    public float[] GetAttackAngles()
    {
        return new float[] { 180f };
    }
}