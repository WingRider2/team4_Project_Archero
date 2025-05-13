using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHP;
    public int currentHP;
    public int attackPower;
    private float attackSpeed = 5f;
    public float AttackSpeed { get => Mathf.Clamp(attackSpeed, 0.2f, 1); set => attackSpeed = value; }
    public int defense;
    private float moveSpeed = 1f;
    public float MoveSpeed { get => Mathf.Clamp(moveSpeed, 5f, 8f); set => moveSpeed = value; }
}