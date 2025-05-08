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


    // id가 같아도 서로 다른 객체로 판단해서 중복으로 hashset에 들어가는것을 막기 위한 오버라이드
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


