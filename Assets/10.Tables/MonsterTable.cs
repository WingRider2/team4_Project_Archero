using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterTable", menuName = "Scriptable Objects/MonsterTable")]
public class MonsterTable : BaseTable<MonsterData>
{
    public override void CreateTable()
    {
        base.CreateTable();
        foreach (var data in dataList)
        {
            DataDic[data.ID] = data;
        }
    }
}
