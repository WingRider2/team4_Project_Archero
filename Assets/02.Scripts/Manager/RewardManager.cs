using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : Singleton<RewardManager>
{
    public void GiveReward(RewardData rewardData)
    {
        AccountManager.Instance.AddGold(rewardData.RewardGold);

        foreach (var item in rewardData.RewardItemsList)
        {
            //TODO : 아이템을 넣어줌
        }
    }
}