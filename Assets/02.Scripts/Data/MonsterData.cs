using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonType
{
    Melee, Range
}

[Serializable]
public class MonsterData
{
    public MonsterBase Monster;
    public int ID;
    public string Name;
    public MonType Type;
    public List<StatData> Stats = new List<StatData>();
    public float AttackRange;
    public float FindRange;
    public float AttackTime;
}