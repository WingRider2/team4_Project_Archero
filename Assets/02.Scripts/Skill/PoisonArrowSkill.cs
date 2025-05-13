using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonArrowSkill : ISkill
{
    public int Id { get; }
    public string Name { get; }
    public string Info { get; }
    public SkillType Type { get; }
    public float Value { get; }
    public Sprite SkillIcon { get; set; }

    public float PosionDPS { get; set; }
    public float Duration { get; set; }

    public PoisonArrowSkill(SkillData data)
    {
        Id = data.Id;
        Name = data.Name;
        Info = data.Info;
        Type = data.Type;
        Value = data.Value;
        SkillIcon = data.SkillIcon;
    }
}
