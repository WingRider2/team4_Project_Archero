using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int SelectedChapter = 1;


    public void StageClear()
    {
        MapManager.Instance.CurrentDoor.DoorControl(true);
        MapManager.Instance.currentStage++;
        UIManager_Battle.Instance.Enable_LevelUp();
    }

    public void ChapterClear(ChapterData chapter)
    {
        RewardManager.Instance.GiveReward(chapter.RewardData);
    }

    public void StartGame()
    {
    }

    public void ReStart()
    {
        MapManager.Instance.GenerateMap(SelectedChapter);
    }
}