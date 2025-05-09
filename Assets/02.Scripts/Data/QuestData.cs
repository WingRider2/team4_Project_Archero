using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData
{
    public int ID;
    public string    QuestName { get; private set; }
    public QuestType QuestType { get; private set; }
    public List<QuestCondition> Conditions; //퀘스트 조건 목록
    public RewardData[] RewardDatas;
    
    
    public bool IsCompleted()
    {
        return Conditions.TrueForAll(x => x.IsCompleted);
    }
}
[Serializable]
public class QuestCondition
{
    public QuestTargetType TargetType;
    public QuestConditionType ConditionType;
    public int TargetID;
    public int CurrentCount = 0;
    public int RequiredCount;
    public bool IsCompleted => CurrentCount >= RequiredCount;
}

[Serializable]
public class RewardData
{
    public int[] RewardItemsList { get; private set; }
    public int RewardGold { get; private set; }
}
