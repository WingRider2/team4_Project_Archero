using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DamageTrap : MonoBehaviour
{
    [SerializeField] private int damage;
    private BoxCollider2D damageColiider;

    public int Damage => damage;

    private void Awake()
    {
        damageColiider = GetComponent<BoxCollider2D>();
        damageColiider.enabled = false;
    }
    
    public void EnableDamageCollider()
    {
        damageColiider.enabled = true;
    }

    public void DisableDamageCollider()
    {
        damageColiider.enabled = false;
    }
    
    
}
