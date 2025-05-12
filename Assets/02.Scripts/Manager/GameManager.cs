using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    void Start()
    {
    }

    void Update()
    {
    }


    public void StageClear()
    {
        MapManager.Instance.CurrentDoor.DoorControl(true);
    }

    public void ChapterClear(ChapterData chapter)
    {
        RewardManager.Instance.GiveReward(chapter.RewardData);
    }
}