using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int SelectedChapter = 1;
    public int BestChapter { get; private set; } = 0;


    public void Start()
    {
        BestChapter = SaveManager.Instance.SaveData.BestChapter;
    }

    public void StageClear()
    {
        MapManager.Instance.CurrentDoor.DoorControl(true);
        if (MapManager.Instance.CurrentChapterData.StageDatas.Count - 1 == MapManager.Instance.currentStage)
        {
            ChapterClear(MapManager.Instance.CurrentChapterData);
            return;
        }

        MapManager.Instance.currentStage++;
        UIManager_Battle.Instance.Enable_LevelUp();
    }

    public void ChapterClear(ChapterData chapter)
    {
        RewardManager.Instance.GiveReward(chapter.RewardData);
        QuestManager.Instance.UpdateCurrentCount(QuestConditionType.ChapterClear, 1);
        if (BestChapter < SelectedChapter)
        {
            BestChapter = SelectedChapter;
        }

        UIManager_Battle.Instance.Enable_GameOver();
    }

    public void ReStart()
    {
        MapManager.Instance.GenerateMap();
    }
}