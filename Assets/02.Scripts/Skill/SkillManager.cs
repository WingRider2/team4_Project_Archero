using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    // 플레이어 레벨업 판단 임시 변수
    bool isPlayerLevelUp;

    // 스테이지 클리어 임시 변수
    bool isStageClear;
    SkillTable skillTable;
    int skillCount = 3;

    private void Awake()
    {
        skillTable = TableManager.Instance.GetTable<SkillTable>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // 레벨업이나 스테이지 클리어 시 선택할 스킬 뽑아주기
    HashSet<SkillData> GetSkillToSelect()
    {
        HashSet<SkillData> selectSkillList = new HashSet<SkillData>();

        if (isPlayerLevelUp || isStageClear)
        {
            while (selectSkillList.Count < 3)
            {
                int skillById = Random.Range(0, skillCount);
                var skill = TableManager.Instance.GetTable<SkillTable>().GetDataByID(skillById);
                selectSkillList.Add(skill);
            }
        }

        return selectSkillList;
    }
}
