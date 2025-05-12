using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField] MonsterBase monPre;
    List<MonsterBase> monsters;

    private void Awake()
    {
  
    }
    public List<MonsterBase> Monsters {  get { return monsters; } }
    public void makeMonList( Dictionary<int,int> mons)
    {
        monsters = new List<MonsterBase>();
        foreach (var mon in mons) {
            for (int i = 0; i < mon.Value; ++i) {
                var m = MakeMon(mon.Key);
                m.OnDeath += HandleMonsterDeath;
                monsters.Add(m);
            }
        }
    }
    public void Clear()
    {
        //뭐 종료시... 작동
    }
    private void HandleMonsterDeath(MonsterBase mon)
    {
        mon.OnDeath -= HandleMonsterDeath;
        Debug.Log($"{mon.name} 삭제");
        monsters.Remove(mon);
        if (monsters.Count == 0) { 
            Clear();
        }
    }
    public MonsterBase MakeMon(int num)
    {
        MonsterData monData= TableManager.Instance.GetTable<MonsterTable>().GetDataByID(num);
        MonsterBase mon = Instantiate(monData.Monster);
        mon.Init(num, monData.Name, monData.HP, monData.ATK,monData.DEF ,monData.MoveSpeed, monData.AttackRange,monData.FindRange);
        if(mon==null)
        {
            Debug.Log("몬스터 생성 실패");
            return null;
        }
        mon.SetTarget(GameObject.FindWithTag("Player").transform);
        return mon;
        
    }
    public void Start()
    {
        MakeMon( 1);
        var ga = MakeMon( 2);
        ga.transform.position = new Vector2(10, 0);
    }
}
