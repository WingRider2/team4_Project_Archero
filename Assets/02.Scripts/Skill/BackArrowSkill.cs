using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackArrowSkill : IAngleArrowSkill, ISkill
{
    public int       Id    { get; set; }
    public string    Name  { get; set; }
    public string    Info  { get; set; }
    public SkillType Type  { get; set; }
    public float     Value { get; set; }

    public Sprite  SkillIcon    { get; set; }
    public float[] angleOffsets { get; } = { 180f };

    public BackArrowSkill(SkillData data)
    {
        Id = data.Id;
        Name = data.Name;
        Info = data.Info;
        Type = data.Type;
        Value = data.Value;
        SkillIcon = data.SkillIcon;
    }


    public float[] GetAttackAngles()
    {
        return angleOffsets;
    }
}