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
}

public class QuestManager : Singleton<QuestManager>
{
    public List<SaveQuestData>                                      CurrentAcceptQuestList { get; private set; } = new List<SaveQuestData>();
    public List<int>                                                ClearQuestList         { get; set; }         = new List<int>();
    public Dictionary<QuestConditionType, List<SaveQuestCondition>> QuestConditionsMap     { get; private set; } = new Dictionary<QuestConditionType, List<SaveQuestCondition>>();

    void Start()
    {
        foreach (var questData in TableManager.Instance.GetTable<QuestTable>().DataDic.Values)
        {
            AcceptQuest(questData);
        }
    }

    public void AcceptQuest(QuestData quest)
    {
        if (CurrentAcceptQuestList.Exists(q => q.ID == quest.ID))
            return;


        SaveQuestData questData = new SaveQuestData(quest);

        SaveQuestCondition condition = questData.Condition;
        if (!QuestConditionsMap.ContainsKey(quest.Condition.ConditionType))
        {
            QuestConditionsMap[quest.Condition.ConditionType] = new List<SaveQuestCondition>();
        }

        QuestConditionsMap[quest.Condition.ConditionType].Add(condition);
        CurrentAcceptQuestList.Add(questData);
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
}