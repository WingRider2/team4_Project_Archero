using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    private MonsterManager monsterManager;

    private void Start()
    {
        monsterManager = MonsterManager.Instance;
    }

    public GameObject FindTarget()
    {
        var enemies = monsterManager.Monsters; // 후에 몬스터 생성 혹은 사망할때 관리;

        GameObject nearest = null;
        float      minDist = Mathf.Infinity;

        foreach (var enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = enemy.gameObject;
            }
        }

        return nearest;
    }
}