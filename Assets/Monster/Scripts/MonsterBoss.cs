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
    public  void Attack(int a)
    {
        
            Debug.Log("공격확인");
            Vector2 dir = Target.position - transform.position;
            if(a==1)
                m_Controller.CircleAttack();
            if(a==2)
                m_Controller.Attack(dir);
       
        
    }
    IEnumerator Timer(float time, System.Action onComplete)
    {
        yield return new WaitForSeconds(time);
        onComplete();
    }
    public bool Teleport(Vector2 pos)
    {
        Debug.Log("이동");
        transform.position = pos+new Vector2(0,1);
        return true;
    }
}
