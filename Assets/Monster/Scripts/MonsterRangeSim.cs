using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRangeSim : MonsterBase
{
    MWeaponHandler m_Controller;

    protected override void Awake()
    {
        base.Awake();

        m_Controller = GetComponentInChildren<MWeaponHandler>();

    }

    public override float Attack()
    {
       
       
            base.Attack();
            Vector2 dir = Target.position - transform.position;
            m_Controller.Attack(dir);
            return MonsterStatManager.GetFinalValue(StatType.AttackPow);
     

    }
}