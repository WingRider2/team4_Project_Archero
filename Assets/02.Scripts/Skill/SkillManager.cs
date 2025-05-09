using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    // �÷��̾� ������ �Ǵ� �ӽ� ����
    bool isPlayerLevelUp;

    // �������� Ŭ���� �ӽ� ����
    bool isStageClear;
    SkillTable skillTable;

    private void Awake()
    {
        skillTable = TableManager.Instance.GetTable<SkillTable>();
    }

    // �������̳� �������� Ŭ���� �� ������ ��ų �̾��ֱ�
    HashSet<SkillData> GetSkillToSelect()
    {
        HashSet<SkillData> selectSkillList = new HashSet<SkillData>();

        if (isPlayerLevelUp || isStageClear)
        {
            while (selectSkillList.Count < 3)
            {
                int skillById = Random.Range(0, skillTable.DataDic.Count + 1);
                var skill = TableManager.Instance.GetTable<SkillTable>().GetDataByID(skillById);
                selectSkillList.Add(skill);
            }
        }

        return selectSkillList;
    }
}
