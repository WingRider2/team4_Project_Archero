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
        if (!isAttack)
        {
            isAttack = true;
            StartCoroutine(Timer(MonsterStatManager.monsterStatDic[StatType.AttackSpd].FinalValue, () => isAttack = false));

            Debug.Log("공격확인");
            Vector2 dir = Target.position - transform.position;
            m_Controller.Attack(dir);
        }
        
    }
    IEnumerator Timer(float time, System.Action onComplete)
    {
        yield return new WaitForSeconds(time);
        onComplete();
    }
    public bool Teleport(Vector2 pos)
    {
        transform.position = pos+new Vector2(0,1);
        return true;
    }
}
