using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows;
using static UnityEditor.Progress;

public enum AttackType
{
    None,
    Arrow,
    PoisonArrow,
    BloodArrow,
}
public class ObjectPool : Singleton<ObjectPool>
{
    //나중에 확장을 위해 흠..
    public GameObject[] prefabs;
    public int poolSize = 30;

    private Dictionary<AttackType, Queue<GameObject>> pools = new();

    void Awake()
    {
        foreach (var item in prefabs)
        {
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(item);
                ProjectileController pc = obj.GetComponent<ProjectileController>();
                obj.transform.parent = transform;
                obj.SetActive(false);
                AttackType attackType = AttackType.None;
                if (Enum.TryParse(item.name, out attackType))
                {
                    if (!pools.ContainsKey(attackType)) pools.Add(attackType, new());
                    pc.attackType = attackType;
                    pools[attackType].Enqueue(obj);
                }
                else
                {
                    Debug.Log("생성 실패");
                }
            }
        }
    }

    public GameObject Get(AttackType attackType)
    {
        if (pools[attackType].Count > 0)
        {
            GameObject obj = pools[attackType].Dequeue();
            obj.SetActive(true);
            return obj;
        }

        GameObject extra = Instantiate(prefabs.FirstOrDefault(item => item.name == attackType.ToString()));
        extra.transform.parent = transform;

        return extra;
    }

    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        ProjectileController pc = obj.GetComponent<ProjectileController>();//추후 화살에 새로운 클래스가 추가 되면 그 클래스로 확인
        pools[pc.attackType].Enqueue(obj);
    }
}
