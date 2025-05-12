using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPart : MonoBehaviour
{
    public float damagetMultiplier = 1f;

    public System.Action<float, Collision2D> OnHit;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnHit?.Invoke(damagetMultiplier, collision);
    }
}
