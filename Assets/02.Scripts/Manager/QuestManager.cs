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
    public List<SaveQuestData> QusetList      { get; private set; } = new List<SaveQuestData>();
    public List<int>           ClearQuestList { get; private set; } = new List<int>();

    private Dictionary<QuestConditionType, List<SaveQuestCondition>> questConditionsMap = new Dictionary<QuestConditionType, List<SaveQuestCondition>>();

    private void Start()
    {
        var questTb = TableManager.Instance.GetTable<QuestTable>();
        foreach (var questData in questTb.DataDic)
        {
            AcceptQuest(questData.Value);
        }
    }

    private void AcceptQuest(QuestData quest)
    {
        if (QusetList.Exists(x => x.ID == quest.ID))
            return;
        SaveQuestData questData = new SaveQuestData(quest);

        SaveQuestCondition condition = questData.Condition;
        if (!questConditionsMap.ContainsKey(quest.Condition.ConditionType))
        {
            questConditionsMap[quest.Condition.ConditionType] = new List<SaveQuestCondition>();
        }

        questConditionsMap[quest.Condition.ConditionType].Add(condition);
        QusetList.Add(questData);
    }

    public void UpdateCurrentCount(QuestConditionType type, int count)
    {
        if (questConditionsMap.TryGetValue(type, out List<SaveQuestCondition> conditions))
        {
            foreach (SaveQuestCondition condition in conditions)
            {
                condition.UpdateCount(count);
            }
        }
    }
}