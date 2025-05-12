using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageEffect :MonoBehaviour
{
    AttackType attackType;

    public abstract void Apply();
}
