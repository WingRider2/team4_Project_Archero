using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum QuestType
{
    Daily,
    Weekly,
    Achievement
}

public enum QuestConditionType
{
    Challenge,
    MonsterKill,
    BossMonsterKill,
    EquipmentLevelUp,
    UserLevelUp,
    ChapterClear,
}

public class QuestManager : Singleton<QuestManager>
{
    public List<SaveQuestData>                                      QusetList          { get; private set; } = new List<SaveQuestData>();
    public List<int>                                                ClearQuestList     { get; set; }         = new List<int>();
    public Dictionary<QuestConditionType, List<SaveQuestCondition>> QuestConditionsMap { get; private set; } = new Dictionary<QuestConditionType, List<SaveQuestCondition>>();

    private void Start()
    {
        var questTb = TableManager.Instance.GetTable<QuestTable>();
        foreach (var questData in questTb.DataDic)
        {
            AcceptQuest(questData.Value);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            UpdateCurrentCount(QuestConditionType.Challenge, 1);
        }
    }

    private void AcceptQuest(QuestData quest)
    {
        if (QusetList.Exists(x => x.ID == quest.ID))
            return;
        SaveQuestData questData = new SaveQuestData(quest);

        SaveQuestCondition condition = questData.Condition;
        if (!QuestConditionsMap.ContainsKey(quest.Condition.ConditionType))
        {
            QuestConditionsMap[quest.Condition.ConditionType] = new List<SaveQuestCondition>();
        }

        QuestConditionsMap[quest.Condition.ConditionType].Add(condition);
        QusetList.Add(questData);
    }

    public void UpdateCurrentCount(QuestConditionType type, int count)
    {
        if (QuestConditionsMap.TryGetValue(type, out List<SaveQuestCondition> conditions))
        {
            foreach (var condition in conditions)
            {
                condition.UpdateCount(count);
            }
        }
    }

    public void GetQuestDataByID(int id)
    {
    }
}