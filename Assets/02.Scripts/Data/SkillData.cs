using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}

[Serializable]
public class SkillData
{
    public int Id;
    public SkillType Type;
    public StatSkillType StatSkillType;
    public string Name;
    public float Value; // 공격 스킬과 스탯 스킬의 증가량 등을 조절하기 위한 변수

    // id가 같아도 서로 다른 객체로 판단해서 중복으로 hashset에 들어가는것을 막기 위한 오버라이드
    // public override bool Equals(object obj)
    // {
    //     if (obj is not SkillData other) return false;
    //     return this.Id == other.Id;
    // }
    //
    // public override int GetHashCode()
    // {
    //     return Id.GetHashCode();
    // }
}