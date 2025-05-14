using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkillManager : SceneOnlyManager<SkillManager>
{
    // 플레이어 레벨업 판단 임시 변수
    private PlayerController player;

    bool isPlayerLevelUp;

    // 스테이지 클리어 임시 변수
    bool isStageClear;
    SkillTable skillTable;


    List<ISkill> skills = new List<ISkill>();
    List<int> skillIDs = new List<int>();
    public List<ISkill> SelectedSKills { get; set; } = new List<ISkill>();

    protected override void Awake()
    {
        player = PlayerController.Instance;
    }

    private void Start()
    {
        skillTable = TableManager.Instance.GetTable<SkillTable>();

        foreach (var skill in skillTable.DataDic.Values)
        {
            skills.Add(CreateSkill(skill));
            skillIDs.Add(skill.Id);
        }
    }

    // 레벨업이나 스테이지 클리어 시 선택할 스킬 뽑아주기
    public HashSet<SkillData> GetSkillToSelect()
    {
        HashSet<SkillData> selectSkillList = new HashSet<SkillData>();

        // !수정 내용!
        // 오류: 스킬 인덱스가 아닌 번호가 나오기도 함
        // 이유: 0 ~ 스킬 인덱스 중 랜덤 값이 나옴. 스킬 인덱스는 0~n, 100~100+n번까지 2가지 유형으로 분포하기에 갯수로만 뽑으면 에러가 남
        // 해결: 스킬 데이터에서 인덱스 값을 모아서 랜덤 돌려버리기

        // 그리고, isPlayerLevelUp, isStageClear로 판정할 필요 없이
        // 스킬 선택이 필요한 상황에서 UIManager_Battle.Instance.Enable_LevelUp(); 사용
        while (selectSkillList.Count < 3)
        {
            //int skillById = Random.Range(0, skillTable.DataDic.Count + 1);

            int skillById = skillIDs[Random.Range(0, skills.Count)];
            var skill = TableManager.Instance.GetTable<SkillTable>().GetDataByID(skillById);
            selectSkillList.Add(skill);
        }

        return selectSkillList;
    }

    private ISkill CreateSkill(SkillData skill)
    {
        return skill.Type switch
        {
            SkillType.Attack when skill.Id == 1 => new TripleArrowSkill(skill),
            SkillType.Attack when skill.Id == 2 => new BackArrowSkill(skill),
            SkillType.Attack when skill.Id == 3 => new SideArrowSkill(skill),
            SkillType.Attack when skill.Id == 4 => new PoisonArrowSkill(skill),
            SkillType.Attack when skill.Id == 5 => new FireArrowSkill(skill),
            SkillType.Attack when skill.Id == 6 => new SlowArrowSkill(skill),

            SkillType.Stat => new StatSkill(skill),

            _ => null
        };
    }

    private ISkill GetSkill(int id)
    {
        return skills.Find(x => x.Id == id);
    }

    public void SelectSkill(int id)
    {
        ISkill skill = GetSkill(id);
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