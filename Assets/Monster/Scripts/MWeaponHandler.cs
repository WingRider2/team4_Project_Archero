using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MWeaponHandler : MonoBehaviour
{
    public float speed;
    public float duration;
    public MprohectileController controller;
    public void Attack(Vector2 dir)
    {
        MprohectileController bullet = Instantiate(controller,transform);
        bullet.Init(dir,this);
    }
}
