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
    public float Value; // ���� ��ų�� ���� ��ų�� ������ ���� �����ϱ� ���� ����

    // id�� ���Ƶ� ���� �ٸ� ��ü�� �Ǵ��ؼ� �ߺ����� hashset�� ���°��� ���� ���� �������̵�
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