using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    Attack,
    StatUp,
}

public class SkillData
{
    public int skillId;
    public string skillName;
    public string skillDescription;
    public int skillLevel;
    public SkillType skillType;
}
