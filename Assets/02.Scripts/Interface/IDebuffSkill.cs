using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDebuffSkill
{
    public DebuffType DebuffType { get; }
    public float      DPS        { get; }
    public float      Duration   { get; set; }

    IDebuffSkill Clone();
}