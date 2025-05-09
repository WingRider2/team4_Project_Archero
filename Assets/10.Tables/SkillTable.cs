using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillTable", menuName = "Scriptable Objects/SkillTable")]
public class SkillTable : BaseTable<SkillData>
{
    public override void CreateTable()
    {
        base.CreateTable();

        foreach (var data in dataList)
        {
            DataDic[data.Id] = data;
        }
    }

    public override SkillData GetDataByID(int id)
    {
        return base.GetDataByID(id);
    }
}
