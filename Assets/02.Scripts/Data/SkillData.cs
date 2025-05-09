using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    Attack,
    Stat
}

[Serializable]
public class SkillData
{
    public int Id;
    public SkillType Type;
    public string Name;
    public float Value; // ���� ��ų�� ���� ��ų�� ������ ���� �����ϱ� ���� ����

    // id�� ���Ƶ� ���� �ٸ� ��ü�� �Ǵ��ؼ� �ߺ����� hashset�� ���°��� ���� ���� �������̵�
    public override bool Equals(object obj)
    {
        if (obj is not SkillData other) return false;
        return this.Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}


