using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerTable", menuName = "Scriptable Objects/PlayerTable")]
public class PlayerTable : BaseTable<PlayerData>
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