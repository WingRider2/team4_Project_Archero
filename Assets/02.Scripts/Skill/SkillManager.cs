using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkillManager : Singleton<SkillManager>
{
    // 플레이어 레벨업 판단 임시 변수
    [SerializeField]
    private PlayerController player;

    bool isPlayerLevelUp;

    // 스테이지 클리어 임시 변수
    bool isStageClear;
    SkillTable skillTable;


    List<ISkill> skills = new List<ISkill>();

    public List<ISkill> SelectedSKills { get; private set; } = new List<ISkill>();

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

    // 레벨업이나 스테이지 클리어 시 선택할 스킬 뽑아주기
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

    public ISkill CreateSkill(SkillData skill)
    {
        return skill.Type switch
        {
            SkillType.Attack when skill.Id == 1 => new TripleArrowSkill(skill),
            SkillType.Attack when skill.Id == 2 => new BackArrowSkill(skill),

            SkillType.Stat => new StatSkill(skill, player),

            _ => null
        };
    }

    public ISkill GetSkill(int id)
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