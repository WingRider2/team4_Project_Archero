using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    public int Id { get; }

    public string Name { get; }
    SkillType Type { get; }

    public float Value { get; }
}