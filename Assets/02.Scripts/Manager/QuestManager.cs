using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum QuestTargetType
{
    
}

public enum QuestConditionType
{
    
}

public enum QuestType
{
    
}
public class QuestManager : Singleton<QuestManager>
{
    
    public Dictionary<(QuestTargetType, QuestConditionType), List<QuestCondition>> QuestConditionsMap
    {
        get;
        private set;
    } = new Dictionary<(QuestTargetType, QuestConditionType), List<QuestCondition>>();
    
    //현재 내가 수락한 실질적인 Quest List
    public List<QuestData> CurrentAcceptQuestList { get; private set; } = new List<QuestData>();
    public List<int>       ClearQuestList         { get; set; }         = new List<int>();
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    /// <summary>
    /// 퀘스트 수락 메서드
    /// </summary>
    /// <param name="quest"></param>
    public void AcceptQuest(QuestData quest)
    {
        if (CurrentAcceptQuestList.Exists(q => q.ID == quest.ID))
            return;


        QuestData questData = quest;
        for (int i = 0; i < quest.Conditions.Count; i++)
        {
            var condition = quest.Conditions[i];
            if (!QuestConditionsMap.ContainsKey((condition.TargetType, condition.ConditionType)))
            {
                QuestConditionsMap[(condition.TargetType, condition.ConditionType)] = new List<QuestCondition>();
            }

            QuestConditionsMap[(condition.TargetType, condition.ConditionType)].Add(condition);
        }
        CurrentAcceptQuestList.Add(quest);
    }
}
