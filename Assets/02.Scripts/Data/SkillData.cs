using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum SkillType
{
    Attack,
    Stat
}

public enum AttackSkillType
{
    None,
    Triple,
    Double,
    Back
}

[Flags]
public enum StatSkillType
{
    None = 0,
    AttackPow = 1 << 0,
    AttackSpd = 1 << 1,
    MoveSpd = 1 << 2,
    MaxHp = 1 << 3,
    CurrentHp = 1 << 4,
}

[Serializable]
public class SkillData
{
    public int Id;
    public SkillType Type;

    public DebuffType DebuffType;
    public List<StatSkillEffect> StatSkillEffects;
    public float Value; // 공격 스킬과 스탯 스킬의 증가량 등을 조절하기 위한 변수
    public string Name;
    public string Info;

    public float dps;
    public float duration;

    public Sprite SkillIcon;
}

[Serializable]
public class StatSkillEffect
{
    public StatType StatType;
    public float Value;
}