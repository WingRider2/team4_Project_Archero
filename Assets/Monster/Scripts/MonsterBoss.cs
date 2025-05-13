using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterBoss : MonsterBase
{

    MWeaponHandler m_Controller;
    protected override void Awake()
    {
        base.Awake();
        m_Controller = GetComponentInChildren<MWeaponHandler>();

    }
    public  float Attack(int a)
    {
        Debug.Log("공격확인");
        Vector2 dir = Target.position - transform.position;
        m_Controller.Attack(dir);
        return atk;
    }
    public bool Teleport(Vector2 pos)
    {
        transform.position = pos+new Vector2(0,1);
        return true;
    }
}
