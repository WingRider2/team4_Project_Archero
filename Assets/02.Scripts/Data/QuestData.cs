using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class QuestData
{
    [SerializeField] public string QuestName;
    public int ID;
    public QuestType QuestType;
    public QuestCondition Condition; //퀘스트 조건 목록
    public RewardData RewardData;
}
[Serializable]
public class QuestCondition
{
    public QuestConditionType ConditionType;
    public int RequiredCount;
    public QuestCondition Clone()
    {
        return (QuestCondition)this.MemberwiseClone();
    }
}

[Serializable]
public class RewardData
{
    public int[] RewardItemsList;
    public int RewardGold;
}


public class SaveQuestData
{
    public int ID;
    public int ClearCount;
    public SaveQuestCondition Condition;
    public bool IsComplete => Condition.IsComplete;
    public SaveQuestData(QuestData questData)
    {
        ID = questData.ID;
        Condition = new SaveQuestCondition(questData.Condition.Clone());
    }
}

public class SaveQuestCondition
{
    public int CurrentCount;
    public int RequiredCount;

    public SaveQuestCondition(QuestCondition condition)
    {
        CurrentCount = 0;
        RequiredCount = condition.RequiredCount;
    }
    public bool IsComplete => CurrentCount >= RequiredCount;

    public void UpdateCount(int count)
    {
        CurrentCount += count;
    }
}
