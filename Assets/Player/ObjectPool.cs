using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows;
//using static UnityEditor.Progress;

public enum PoolType
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

    private Dictionary<PoolType, Queue<GameObject>> pools = new();

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
                PoolType poolType = PoolType.None;
                if (Enum.TryParse(item.name, out poolType))
                {
                    if (!pools.ContainsKey(poolType)) pools.Add(poolType, new());
                    pc.poolType = poolType;
                    pools[poolType].Enqueue(obj);
                }
                else
                {
                    Debug.Log("생성 실패");
                }
            }
        }
    }

    public GameObject Get(PoolType poolType)
    {
        if (pools[poolType].Count > 0)
        {
            GameObject obj = pools[poolType].Dequeue();
            obj.SetActive(true);
            return obj;
        }

        GameObject extra = Instantiate(prefabs.FirstOrDefault(item => item.name == poolType.ToString()));
        extra.transform.parent = transform;

        return extra;
    }

    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        ProjectileController pc = obj.GetComponent<ProjectileController>();
        pools[pc.poolType].Enqueue(obj);
    }
}
