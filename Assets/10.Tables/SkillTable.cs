using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTable : BaseTable<SkillData>
{
    public override void CreateTable()
    {
        base.CreateTable();

        foreach (var data in dataList)
        {
            DataDic[data.skillId] = data;
        }
    }
}
