using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class StatSkill : ISkill
{
    public int       Id    { get; }
    public string    Name  { get; }
    public string    Info  { get; }
    public SkillType Type  { get; }
    public float     Value { get; }

    private readonly PlayerController player;
    private List<StatSkillEffect> Effects { get; }

    public StatSkill(SkillData data)
    {
        Id = data.Id;
        Name = data.Name;
        Type = data.Type;
        this.player = PlayerController.Instance;
        Effects = data.StatSkillEffects;
    }

    public void ApplyStat()
    {
        foreach (var effect in Effects)
        {
            player.PlayerStats.ApplyStatEffect(effect.StatType, StatValueType.Buff, effect.Value);
        }
    }
}