using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISKill
{
    public int Id { get; set; }

    public string Name { get; set; }
    SkillType Type { get; set; }

    public float Value { get; set; }
}
