using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField] MonsterBase monPre;

    private void Awake()
    {
  
    }
    public bool MakeMon(Vector2 pos,int num)
    {
        MonsterData monData= TableManager.Instance.GetTable<MonsterTable>().GetDataByID(num);
        MonsterBase mon = Instantiate(monData.Monster, pos, Quaternion.identity);
        mon.Init(num, monData.Name, monData.HP, monData.ATK,monData.DEF ,monData.MoveSpeed, monData.AttackRange,monData.FindRange);
        if(mon==null)
        {
            Debug.Log("몬스터 생성 실패");
            return false;
        }
        mon.SetTarget(GameObject.FindWithTag("Player").transform);
        return true;
        
    }
    public void Start()
    {
        MakeMon(Vector2.zero,1);
        MakeMon(new Vector2(10,0), 2);
    }
}
