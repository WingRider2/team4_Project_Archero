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
    public int NextValue;
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
        Condition = new SaveQuestCondition(questData.Condition.Clone(), questData.QuestType);
    }

    public void ClearQuest()
    {
        while (Condition.TryClear())
        {
            var questData = TableManager.Instance.GetTable<QuestTable>().GetDataByID(ID);
            if (questData == null)
                return;
            //TODO: 리워드 추가
            var rewordData = questData.RewardData;
            if (rewordData == null)
                return;
            RewardManager.Instance.GiveReward(rewordData);
            ClearCount++;
        }
    }
}

public class SaveQuestCondition
{
    public int CurrentCount;
    public int RequiredCount;


    private readonly int nextValue;
    private readonly QuestType questType;

    public SaveQuestCondition(QuestCondition condition, QuestType questType)
    {
        CurrentCount = 0;
        RequiredCount = condition.RequiredCount;
        nextValue = condition.NextValue;
        this.questType = questType;
    }

    public bool IsComplete => CurrentCount >= RequiredCount;

    public void UpdateCount(int count)
    {
        CurrentCount += count;

        if (CurrentCount >= RequiredCount)
        {
            if (nextValue == 0)
                CurrentCount = RequiredCount;
        }
    }

    public bool TryClear()
    {
        if (CurrentCount < RequiredCount)
            return false;

        if (questType != QuestType.Achievement)
            CurrentCount -= RequiredCount;

        if (nextValue > 0)
            RequiredCount += nextValue;

        return true;
    }
}