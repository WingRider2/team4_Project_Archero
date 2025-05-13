using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAngleArrowSkill
{
    float[] angleOffsets { get; }
    float[] GetAttackAngles();
}