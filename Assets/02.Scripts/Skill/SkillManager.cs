using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkillManager : Singleton<SkillManager>
{
    // �÷��̾� ������ �Ǵ� �ӽ� ����
    [SerializeField]
    private PlayerController player;

    bool isPlayerLevelUp;

    // �������� Ŭ���� �ӽ� ����
    bool isStageClear;
    SkillTable skillTable;


    List<ISKill> skills = new List<ISKill>();

    public List<ISKill> SelectedSKills { get; private set; } = new List<ISKill>();

    private void Awake()
    {
    }

    private void Start()
    {
        skillTable = TableManager.Instance.GetTable<SkillTable>();

        foreach (var skill in skillTable.DataDic.Values)
        {
            skills.Add(CreateSkill(skill));
        }
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
                var skill     = TableManager.Instance.GetTable<SkillTable>().GetDataByID(skillById);
                selectSkillList.Add(skill);
            }
        }

        return selectSkillList;
    }

    public ISKill CreateSkill(SkillData skill)
    {
        return skill.Type switch
        {
            SkillType.Attack when skill.Id == 1 => new TripleArrowSkill(skill),
            SkillType.Attack when skill.Id == 2 => new BackArrowSkill(skill),

            SkillType.Stat => new StatSkill(skill, player),

            _ => null
        };
    }

    public ISKill GetSkill(int id)
    {
        return skills.Find(x => x.Id == id);
    }

    public void SelecteSkill(int id)
    {
        var skill = GetSkill(id);
        if (skill == null)
            return;

        SelectedSKills.Add(skill);

        if (skill is StatSkill statSkill)
        {
            statSkill.ApplyStat();
        }
        else
        {
            skills.Remove(skill);
        }
    }
}