using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillData
{
    public int id;
    public string name;
    public string description;
    public int level;


    // id�� ���Ƶ� ���� �ٸ� ��ü�� �Ǵ��ؼ� �ߺ����� hashset�� ���°��� ���� ���� �������̵�
    public override bool Equals(object obj)
    {
        if (obj is not SkillData other) return false;
        return this.id == other.id;
    }

    public override int GetHashCode()
    {
        return id.GetHashCode();
    }

    public class AttackSkill
    {

    }
}


