using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestTable", menuName = "Scriptable Objects/QuestTable")]
public class QuestTable : BaseTable<QuestData>
{
    public override void CreateTable()
    {
        base.CreateTable();
        foreach (QuestData questData in dataList)
        {
            DataDic[questData.ID] = questData;
        }
    }
}
