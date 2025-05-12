using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterManager : SceneOnlyManager<MonsterManager>
{
    List<MonsterBase> monsters;

    protected override void Awake()
    {
    }

    public List<MonsterBase> Monsters { get { return monsters; } }

    public void makeMonList(List<Vector3> monpoint, int level)
    {
        monsters = new List<MonsterBase>();
        foreach (var monPos in monpoint)
        {
            monsters.Add(MakeMon(monPos, Random.Range(1, 3)));
        }
    }

    public void Clear()
    {
        //�� �����... �۵�
    }

    private void HandleMonsterDeath(MonsterBase mon)
    {
        mon.OnDeath -= HandleMonsterDeath;
        Debug.Log($"{mon.name} ����");
        //TODO : 보스몬스터일때 보스 퀘스트로 증가
        QuestManager.Instance.UpdateCurrentCount(QuestConditionType.MonsterKill, 1);
        monsters.Remove(mon);
        if (monsters.Count == 0)
        {
            Clear();
        }
    }

    public MonsterBase MakeMon(Vector3 pos, int num)
    {
        MonsterData monData = TableManager.Instance.GetTable<MonsterTable>().GetDataByID(num);
        MonsterBase mon     = Instantiate(monData.Monster, pos, Quaternion.identity);
        mon.Init(num, monData.Name, monData.HP, monData.ATK, monData.DEF, monData.MoveSpeed, monData.AttackRange, monData.FindRange);
        if (mon == null)
        {
            Debug.Log("���� ���� ����");
            return null;
        }

        mon.SetTarget(GameObject.FindWithTag("Player").transform);
        return mon;
    }
    //public void Start()
    //{
    //    MakeMon(new Vector2(0, 0), 1);
    //    var ga = MakeMon(new Vector2(10, 0), 2);

    //}
    protected override void OnDestroy()
    {
    }
}