using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

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

    public void MakeBossMonster(Vector3 spawnPos)
    {
        monsters.Add(MakeMon(spawnPos, 3));
    }

    private void Update()
    {
        //Test
        if (Input.GetKeyDown(KeyCode.F9))
        {
            monsters.Find(x => !x.IsDead)?.Damaged(1000);
        }
    }

    public void Clear()
    {
        //�� �����... �۵�
    }

    private void HandleMonsterDeath(MonsterBase mon)
    {
        mon.OnDeath -= HandleMonsterDeath;
        Debug.Log($"{mon.name} 몬스터 사망");
        //TODO : 보스몬스터일때 보스 퀘스트로 증가
        QuestManager.Instance.UpdateCurrentCount(QuestConditionType.MonsterKill, 1);
        monsters.Remove(mon);
        if (monsters.Count == 0)
        {
            Clear();
            GameManager.Instance.StageClear();
        }
    }

    public MonsterBase MakeMon(Vector3 pos, int num)
    {
        MonsterData monData = TableManager.Instance.GetTable<MonsterTable>().GetDataByID(num);
        MonsterBase mon     = Instantiate(monData.Monster, pos, Quaternion.identity);
        mon.Init(monData);
        mon.OnDeath += HandleMonsterDeath;
        if (mon == null)
        {
            Debug.Log("���� ���� ����");
            return null;
        }

        mon.SetTarget(PlayerController.Instance.transform);
        return mon;
    }


    public void Start()
    {
        // MakeMon(new Vector2(0, 0), 1);
        // var ga = MakeMon(new Vector2(10, 0), 2);
        // MakeMon(new Vector2(5, 0), 3);
    }

    protected override void OnDestroy()
    {
    }
}