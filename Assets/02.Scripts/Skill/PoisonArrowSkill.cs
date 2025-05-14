using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonArrowSkill : ISkill, IDebuffSkill
{
    private readonly SkillData data;
    public int Id { get; }
    public string Name { get; }
    public string Info { get; }
    public SkillType Type { get; }
    public float Value { get; }
    public Sprite SkillIcon { get; set; }
    public DebuffType DebuffType { get; }
    public float DPS { get; }
    public float Duration { get; set; }

    public IDebuffSkill Clone()
    {
        return new PoisonArrowSkill(data);
    }

    public PoisonArrowSkill(SkillData data)
    {
        this.data = data;

        Id = data.Id;
        Name = data.Name;
        Info = data.Info;
        Type = data.Type;
        Value = data.Value;
        SkillIcon = data.SkillIcon;
        DPS = data.dps;
        Duration = data.duration;
        DebuffType = data.DebuffType;
    }
}